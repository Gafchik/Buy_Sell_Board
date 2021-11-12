using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Buy_Sell_Board.Models.Announcement
{
    public class Announcement
    {
        [Column(TypeName = "int")]// тип данных в SQL
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(450)")]// тип данных в SQL
        public string User_Id { get; set; }

        [Column(TypeName = "int")]// тип данных в SQL
        public int Category_Id { get; set; }

        [Column(TypeName = "int")]// тип данных в SQL
        public int Subcategory_Id { get; set; }
        [Column(TypeName = "nvarchar(50)")]// тип данных в SQL
        public string Product_Name{ get; set; }

        [Column(TypeName = "nvarchar(50)")]// тип данных в SQL
        public string Product_Model { get; set; }

        [Column(TypeName = "nvarchar(max)")]// тип данных в SQL
        public string Description { get; set; }

        [Column(TypeName = "float")]// тип данных в SQL
        public float Price { get; set; }

    }
}
