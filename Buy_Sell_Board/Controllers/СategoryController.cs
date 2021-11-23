using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Buy_Sell_Board.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;


namespace Buy_Sell_Board.Controllers
{
    public class СategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public СategoryController(ApplicationDbContext context) { _db = context; }
        // загружает подкатегориии при первом гет запросе с помощью  аякса
        public async Task<JsonResult> OnPostFirstCat([FromBody] JsonElement json) => new JsonResult(await _db.Categorys.ToListAsync());

        // загружает подкатегориии при первом гет запросе с помощью  аякса
        public  JsonResult OnPostFirstSubCat([FromBody] JsonElement json) => new JsonResult(_db.Subcategorys.ToList().FindAll(i => i.Category_Id == 1));

        // RequestVerificationToken метод обновления подкатегорий
        public  JsonResult OnPostSubCat([FromBody] JsonElement json)
        {
            // парсим входящий json в строку истроку парсим в динамик
            dynamic data = JObject.Parse(JsonSerializer.Deserialize<object>(json.GetRawText()).ToString());
            // с динамик в нужный тип данных
            var SellCat = int.Parse(data.SelectedValue.Value);
            // делакм выборку с БД и в json возвращаем
            return new JsonResult(_db.Subcategorys.ToList().FindAll(i => i.Category_Id == SellCat));
        }
    }
}
