using Buy_Sell_Board.Models.Announcement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buy_Sell_Board.Models.API_Model
{
    public class API_Announcement_Model
    {    
        public int Id { get; set; }    
        public string User_Name { get; set; }      
        public string Category { get; set; }      
        public string Subcategory { get; set; }    
        public string Product_Name { get; set; }
        public string Product_Model { get; set; }      
        public string Description { get; set; }    
        public float Price { get; set; }
        public List<Image> Img_url { get; set; }
    }
}
