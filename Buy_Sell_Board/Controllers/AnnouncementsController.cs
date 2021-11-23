using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Buy_Sell_Board.Data;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using Buy_Sell_Board.Models.API_Model;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Buy_Sell_Board.Models.Announcement;
using Microsoft.AspNetCore.Identity;
using Buy_Sell_Board.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Buy_Sell_Board.Areas.Identity.Pages.Account.Manage;
using System;
using System.Net.Http;
using System.Net;
using System.Diagnostics;

namespace Buy_Sell_Board.Controllers
{

    public class AnnouncementsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IWebHostEnvironment _appEnvironment; // интерфейс для окружения
                                                              // коструктор со всем добром которое выше
        public AnnouncementsController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ApplicationDbContext db,
             IWebHostEnvironment appEnvironment)
        {
            _db = db;
            _appEnvironment = appEnvironment;
            _userManager = userManager;
            _signInManager = signInManager;
           
        }

      
        [Authorize]
        public async Task<IActionResult> Create(New_AnnouncementModel.InputModel Input)
        {
            
            try
            {
                if (ModelState.IsValid)
                {

                    _db.Announcements.Add(new Announcement
                    {
                        User_Id = _userManager.GetUserId(User),
                        Category_Id = Input.CategorySelectedValue,
                        Subcategory_Id = Input.SubcategorySelectedValue,
                        Product_Name = Input.Product_Name,
                        Product_Model = Input.Product_Model,
                        Description = Input.Description,
                        Price = Input.Price
                    });
                    _db.SaveChanges();
                    int new_Announcement_Id = _db.Announcements.ToList().Find(i =>
                           i.User_Id == _userManager.GetUserId(User) &&
                           i.Category_Id == Input.CategorySelectedValue &&
                           i.Subcategory_Id == Input.SubcategorySelectedValue &&
                           i.Product_Name == Input.Product_Name &&
                           i.Product_Model == Input.Product_Model &&
                           i.Description == Input.Description &&
                           i.Price == Input.Price).Id;

                    #region file         
                    foreach (var img in Input.Files)
                    {
                        // путь к папке Image
                        string path = "/Image/" + img.FileName;
                        // сохраняем файл в папку Files в каталоге wwwroot
                        using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                            await img.CopyToAsync(fileStream);
                        // ~ нам говорит о том что мы попадаем в папку wwwroot
                        //product.img = "~" + path;
                        _db.Images.Add(new Image
                        {
                            Path = path,
                            Announcement_Id = new_Announcement_Id
                        });
                    }
                    #endregion
                    await _db.SaveChangesAsync();
                }
                
            }
            catch (Exception) {}
            return Redirect("/Identity/Account/Manage");

        }

       

        public JsonResult PostAnnouncement([FromBody] JsonElement json)
        {

            // парсим входящий json в строку истроку парсим в динамик
            dynamic data = JObject.Parse(JsonSerializer.Deserialize<object>(json.GetRawText()).ToString());
            // с динамик в нужный тип данных
            string name = data.Name.Value;
            int Cut = int.Parse(data.CategoryID.Value);
            int SubCut = int.Parse(data.SubCategoryID.Value);
            List<API_Announcement_Model> announcements = new List<API_Announcement_Model>();
            if(name == string.Empty || name=="")
            {
                _db.Announcements.ToList().FindAll(i => i.Category_Id == Cut && i.Subcategory_Id == SubCut).
                       ForEach(i => announcements.Add(new API_Announcement_Model
                       {
                           Id = i.Id,
                           User_Name = _db.Users.ToList().Find(j => j.Id == i.User_Id).FirstName,
                           Category = _db.Categorys.ToList().Find(j => j.Id == i.Category_Id).Name,
                           Subcategory = _db.Subcategorys.ToList().Find(j => j.Id == i.Subcategory_Id).Name,
                           Product_Name = i.Product_Name,
                           Product_Model = i.Product_Model,
                           Description = i.Description,
                           Price = i.Price,
                           Img_url = _db.Images.ToList().FindAll(j => j.Announcement_Id == i.Id)

                       }));
               
            }
            else
            {
                _db.Announcements.ToList().FindAll(i => i.Category_Id == Cut && i.Subcategory_Id == SubCut&&i.Product_Name.Contains(name)).
                      ForEach(i => announcements.Add(new API_Announcement_Model
                      {
                          Id = i.Id,
                          User_Name = _db.Users.ToList().Find(j => j.Id == i.User_Id).FirstName,
                          Category = _db.Categorys.ToList().Find(j => j.Id == i.Category_Id).Name,
                          Subcategory = _db.Subcategorys.ToList().Find(j => j.Id == i.Subcategory_Id).Name,
                          Product_Name = i.Product_Name,
                          Product_Model = i.Product_Model,
                          Description = i.Description,
                          Price = i.Price,
                          Img_url = _db.Images.ToList().FindAll(j => j.Announcement_Id == i.Id)

                      }));
            }
            return new JsonResult(announcements);
        }      
           
        
      

       
        
        
        #region Аякс ЗАпросы
       
        #endregion


    }
}
