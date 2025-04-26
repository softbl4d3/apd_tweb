using eUseControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;


namespace eUseControl.Domain.Entities.User
{

    [Table("Users")]
    public class UserDbTable : BaseDbItem
    {
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Несоответсвующий логин ")]
        public string UserName { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Несоответсвующий пароль")]
        public string Password { get; set; }
        [Required]
        public URole Role { get; set; }
    }
}
