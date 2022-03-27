using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecurityMine.Models
{
    public class NewAspUser
    {
        [Required(ErrorMessage ="This Field is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "This Field is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "This Field is required")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "This Field is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Password must be between 6 and 20 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "This Field is required")]
        [DataType(DataType.Password)]
        public string RetypePassword { get; set; }
    }

    public class ChangePasswordValidation
    {
        [Required(ErrorMessage = "This Field is required")]
        [DataType(DataType.Password)]
        public string Current_Password { get; set; }

        [Required(ErrorMessage = "This Field is required")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Password must be between 6 and 20 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.")]
        public string New_Password { get; set; }

        [Required(ErrorMessage = "This Field is required")]
        [DataType(DataType.Password)]
        public string Retype_Password { get; set; }
    }

    public class ResetPasswordValidation
    {
        [Required(ErrorMessage = "This Field is required")]
        public string UserName { get; set; }
        
        [Required(ErrorMessage = "This Field is required")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Password must be between 6 and 20 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.")]
        public string New_Password { get; set; }

        [Required(ErrorMessage = "This Field is required")]
        [DataType(DataType.Password)]
        public string Retype_Password { get; set; }
    }

    public class SendMessageValidation
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Message { get; set; }
    }

    public class ReadMessagesValidation
    {
        [Required(ErrorMessage = "This Field is required")]
        public string UserName { get; set; }
    }

    public class FeedBackValidation
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FeedBack { get; set; }
    }

    public class LockoutValidation
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string LockoutStateEnabled { get; set; }
    }
}