using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace WebSite.Controllers
{
    public class RoleController : Controller
    {
        //private readonly IUserService _userService;

        //public RoleController(IUserService userService)
        //{
        //    _userService = userService;
        //}

        // GET: User
        public ActionResult Index()
        {
            //return View(_userService.GetAll());
            return View();
        }

        //// GET: User/Details/5
        //public ActionResult Details(int id)
        //{
        //    var product = _userService.GetById(id);
        //    if (product == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(product);
        //}

        //// GET: User/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: User/Create
        //[HttpPost]
        //public ActionResult Create(User user)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here
        //        product.Id = 0;
        //        _productApp.Save(product);
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
        //    }
        //}

        //// GET: User/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    var product = _userService.GetById(id);

        //    if (product == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(product);
        //}

        //// POST: Product/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, User user)
        //{
        //    try
        //    {                
        //        _userService.Save(user);
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
        //    }
        //}

        //// GET: Product/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Product/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}