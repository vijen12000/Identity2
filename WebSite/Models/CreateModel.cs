using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebSite.Models
{
    public class CreateUserModel
    {
        [Required]
        public string Email { get; set; }

        [Required, Display(Name = "Full name")]
        public string FullName { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, Display(Name = "Confirm password"),DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }

    public class EditUserModel
    {
        [HiddenInput]
        public string Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required, Display(Name = "Full name")]
        public string FullName { get; set; }        
    }

    public class ViewUserModel
    {
        [ReadOnly(true)]
        public string Email { get; set; }

        [ReadOnly(true), Display(Name = "Full name")]
        public string FullName { get; set; }        
    }

}