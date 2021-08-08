using System.ComponentModel.DataAnnotations;

namespace Learning.WebSite.Models
{
    public class RegisterModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }
    }
}