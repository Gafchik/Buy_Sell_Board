using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Buy_Sell_Board.Models.Announcement
{
    public class Image
    {
        [Column(TypeName = "int")]// тип данных в SQL
        public int Id { get; set; }

        [Column(TypeName = "int")]// тип данных в SQL
        public int Announcement_Id { get; set; }

        [Column(TypeName = "nvarchar(max)")]// тип данных в SQL
        public string Path { get; set; }
    }
}
