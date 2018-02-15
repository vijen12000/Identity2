using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebSite.Core;
using WebSite.Core.Model;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class UserController : Controller
    {
        private readonly AppUserManager _userManager;

        public UserController(AppUserManager userManager)
        {
            this._userManager = userManager;
        }

        public ActionResult Index()
        {
            var users = _userManager.Users;

            return View(users);
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> New(CreateUserModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var identiyUser = await _userManager.FindByEmailAsync(model.Email);
            if (identiyUser != null)
            {
                return RedirectToAction("Index", "User");
            }

            var user = new ExtendedUser
            {
                Email= model.Email,
                FullName = model.FullName,
                UserName=model.Email,
                Id=Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            //Genrate Token
            var token = _userManager.GenerateEmailConfirmationTokenAsync(user.Id);
            var confirmUrl = Url.Action("ConfirmEmail", "Account", new { userid = user.Id, token = token }, Request.Url.Scheme);

            await _userManager.SendEmailAsync(user.Id, "Email Confirmation", $"Use link to confirm email:{confirmUrl}");

            if (result.Succeeded) return RedirectToAction("Index", "User");

            ModelState.AddModelError("", result.Errors.FirstOrDefault());

            return View(model);
        }

        public async Task<ActionResult> View(string id)
        {            
            var identiyUser = await _userManager.FindByIdAsync(id);
            if (identiyUser == null)
            {
                return RedirectToAction("Index", "User");
            }

            var user = new ViewUserModel
            {
                Email = identiyUser.Email,
                FullName = identiyUser.FullName,                
            };

            return View(user);
        }

        public async Task<ActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                RedirectToAction("Index", "User");
            }
            var identiyUser = await _userManager.FindByIdAsync(id);
            if (identiyUser == null)
            {
                return RedirectToAction("Index", "User");
            }

            var user = new EditUserModel
            {
                Email = identiyUser.Email,
                FullName = identiyUser.FullName,
                Id=identiyUser.Id
            };

            return View(user);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(EditUserModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.Id);

            //TODO:Give Feedback, may be deleted
            if (user == null)
            {
                return RedirectToAction("Index", "User");
            }

            user.Email = model.Email;
            user.FullName = model.FullName;
            
            var result = await _userManager.UpdateAsync(user);

            return RedirectToAction("Index", "User");
        }


        public async Task<ActionResult> Delete(string id)
        {
            var identiyUser = await _userManager.FindByIdAsync(id);
            if (identiyUser == null)
            {
                return RedirectToAction("Index", "User");
            }
            var result = _userManager.DeleteAsync(identiyUser);
            return RedirectToAction("Index", "User");
        }

        public async Task<ActionResult> ConfirmEmail(string userId, string token)
        {
            var result = await _userManager.ConfirmEmailAsync(userId, token);
            if (!result.Succeeded)
            {
                return await View("Error");
            }
            return RedirectToAction("Index", "User");
        }
    }
}