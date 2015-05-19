using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WatDuangDee.Domain.Entities;

namespace WatDuangDee.UI.Models
{


    public class AdminViewModel
    {

        [Required]
        [Display(Name = "Name (ชื่อ) :")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Surname (นามสกุล) :")]
        public string Surname { get; set; }
        [Required]
        [Display(Name = "Email (อีเมล์) :")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Username (ชื่อผู้ใช้) :")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password (รหัสผ่าน) :")]
        public string Password { get; set; }
    }

    public class ManageUserViewModel
    {
      
        

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password (รหัสผ่านเดิม) :")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password (รหัสผ่านใหม่) :")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password (ยืนยันรหัสผ่านใหม่) :")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        [Display(Name = "Name (ชื่อ) :")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Surname (นามสกุล) :")]
        public string Surname { get; set; }
        [Required]
        [Display(Name = "Email (อีเมล์) :")]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class LoginViewModel 
    {


        [Required]
        [Display(Name = "Username (ชื่อผู้ใช้) :")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password (รหัสผ่าน) :")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel  
    {
         
        [Required]
        [Display(Name = "Username (ชื่อผู้ใช้) :")]
        [Index(IsUnique = true)]
        [StringLength(100)] 
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password (รหัสผ่าน) :")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

         [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password (ยืนยันรหัสผ่าน) :")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
         [Required]
         [Display(Name = "Name (ชื่อ) :")]
        public string Name { get; set; }
         [Required]
         [Display(Name = "Surname (นามสกุล) :")]
        public string Surname { get; set; }
         [Required]
         [Display(Name = "Email (อีเมล์) :")]
         [EmailAddress]
        public string Email { get; set; }
    }
    public class QAViewModel
    {

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual QuestionAnswerDhamma QuestionAnswerDhamma { get; set; }
    }

    public class IndexViewModel
    {

        public virtual IEnumerable<News> News { get; set; }
        public virtual IEnumerable<Activity> Activity { get; set; }
    }

}
