using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Buy_Sell_Board.Models
{
    public class AppUser : IdentityUser
    {
        [PersonalData]// флаг показующий что это поле юзера
        [Column(TypeName = "nvarchar(50)")]// тип данных в SQL
        public string FirstName { get; set; }


        [PersonalData]// флаг показующий что это поле юзера
        [Column(TypeName = "nvarchar(50)")]// тип данных в SQL
        public string LastName { get; set; }


        [PersonalData]// флаг показующий что это поле юзера
        [Column(TypeName = "date")]// тип данных в SQL
        public DateTime Date_of_Birth { get; set; }

        [PersonalData]// флаг показующий что это поле юзера
        [Column(TypeName = "nvarchar(50)")]// тип данных в SQL
        public string Region { get; set; }

        [PersonalData]// флаг показующий что это поле юзера
        [Column(TypeName = "nvarchar(50)")]// тип данных в SQL
        public string City { get; set; }
    }
}
