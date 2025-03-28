using System.ComponentModel.DataAnnotations;
using TWeb48.Models;
namespace eUseControl.Web.Models
{
    public class User : BaseDbItem
    {

        [Required]
        [Display(Name = "Логин")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Роль")]
        [RegularExpression("^(Admin|Chef|Waiter)$", ErrorMessage = "Role must be either 'Admin', 'Chef', or 'Waiter'.")]
        public string Role { get; set; }
    }
}
