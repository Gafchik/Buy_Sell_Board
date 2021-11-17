using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Buy_Sell_Board.Data;
using Buy_Sell_Board.Models.Announcement;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using Buy_Sell_Board.Models.API_Model;

namespace Buy_Sell_Board.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public AnnouncementsController(ApplicationDbContext context)
        {
            _db = context;
        }

        // GET: api/Announcements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Announcement>>> GetAnnouncements()
        {
            return await _db.Announcements.ToListAsync();
        }

        // GET: api/Announcements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<int>> GetAnnouncement(int id)
        {

            return 1;
        }

        // PUT: api/Announcements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnnouncement(int id, Announcement announcement)
        {
            if (id != announcement.Id)
            {
                return BadRequest();
            }

            _db.Entry(announcement).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnnouncementExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

       
        [HttpPost]
        public  JsonResult PostAnnouncement([FromBody] JsonElement json)
        {
            // парсим входящий json в строку
            var jsonStr = System.Text.Json.JsonSerializer.Deserialize<object>(json.GetRawText()).ToString();
            //строку парсим в динамик
            dynamic data = JObject.Parse(jsonStr);
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
            // DELETE: api/Announcements/5
            [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncement(int id)
        {
            var announcement = await _db.Announcements.FindAsync(id);
            if (announcement == null)
            {
                return NotFound();
            }

            _db.Announcements.Remove(announcement);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        private bool AnnouncementExists(int id)
        {
            return _db.Announcements.Any(e => e.Id == id);
        }
    }
}
