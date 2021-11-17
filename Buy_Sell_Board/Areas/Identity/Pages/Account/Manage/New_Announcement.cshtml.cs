using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Buy_Sell_Board.Data;
using Buy_Sell_Board.Models;
using Buy_Sell_Board.Models.Announcement;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Buy_Sell_Board.Areas.Identity.Pages.Account.Manage
{
    public class New_AnnouncementModel : PageModel
    {
        // логеры и менеджеры
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<ChangePasswordModel> _logger;
        public ApplicationDbContext _db;
       /* public SelectList CategoryActionsList { get; set; }// категория выпадающий список
        public SelectList SubcategoryActionsList { get; set; }// подкатегория выпадающий список*/
        private readonly IWebHostEnvironment _appEnvironment; // интерфейс для окружения


        // коструктор со всем добром которое выше
        public New_AnnouncementModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ApplicationDbContext db,
             IWebHostEnvironment appEnvironment,
            ILogger<ChangePasswordModel> logger)
        {
            _db = db;
            _appEnvironment = appEnvironment;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            /*Input = new InputModel();// обязательно выделять память для выпадающих списков
            // конструктор выпадающих списков (Коллекция откуда брать, Имя параметра Ключ, Значение, Куда совать выбраный value)
            CategoryActionsList = new SelectList(_db.Categorys, "Id", "Name", Input.CategorySelectedValue);*/
        }


        [BindProperty] // флаг котороый говорт что это свойство модель биндиться к вью
        public InputModel Input { get; set; } // сам проп бинда 
        public string ReturnUrl { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        [TempData]  //хз
        public string StatusMessage { get; set; }



        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

        }
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
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
            return Page();
        }
        #region Аякс ЗАпросы
        [HttpPost] // загружает подкатегориии при первом гет запросе с помощью  аякса
        public JsonResult OnPostFirstCat([FromBody] JsonElement json) => new JsonResult(_db.Categorys.ToList());

        [HttpPost] // загружает подкатегориии при первом гет запросе с помощью  аякса
        public JsonResult OnPostFirstSubCat([FromBody] JsonElement json) => new JsonResult(_db.Subcategorys.ToList().FindAll(i => i.Category_Id == 1));

        [HttpPost] // RequestVerificationToken метод обновления подкатегорий
        public JsonResult OnPostSubCut([FromBody] JsonElement json)
        {
            // парсим входящий json в строку
            var jsonStr = System.Text.Json.JsonSerializer.Deserialize<object>(json.GetRawText()).ToString();
            //строку парсим в динамик
            dynamic data = JObject.Parse(jsonStr);
            // с динамик в нужный тип данных
            var SellCat = int.Parse(data.SelectedValue.Value);
            // делакм выборку с БД и в json возвращаем
            return new JsonResult(_db.Subcategorys.ToList().FindAll(i => i.Category_Id == SellCat));
        }
        #endregion
        // класс нашей импут модели
        public class InputModel
        {

            [Required]
            [Display(Name = "Category")]
            public int CategorySelectedValue { get; set; }   // категория айди  

            [Required]
            [Display(Name = "Subcategory")]
            public int SubcategorySelectedValue { get; set; }   // подкатегория айди  

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Product_Name")]
            public string Product_Name { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Product_Model")]
            public string Product_Model { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Description")]
            public string Description { get; set; }

            [Required]
            [Display(Name = "Price")]
            public float Price { get; set; }

            [Required]
            [Display(Name = "Photo")]
            public IFormFileCollection Files { get; set; } // бинд  коллекции загруженых файлов
        }
    }
}

