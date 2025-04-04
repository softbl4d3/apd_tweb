using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using eUseControl.Domain.Enums;

namespace eUseControl.Domain.Entities.DTO
{
    public class EmployeeRegistrationDTO
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
        public URole Role { get; set; }

    }
}