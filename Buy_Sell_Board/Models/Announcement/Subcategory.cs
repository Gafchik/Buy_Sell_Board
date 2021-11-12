using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Buy_Sell_Board.Models.Announcement
{
    public class Subcategory
    {
        [Column(TypeName = "int")]// тип данных в SQL
        public int Id { get; set; }

        [Column(TypeName = "int")]// тип данных в SQL
        public int Category_Id { get; set; }

        [Column(TypeName = "nvarchar(50)")]// тип данных в SQL
        public string Name { get; set; }
    }
}
