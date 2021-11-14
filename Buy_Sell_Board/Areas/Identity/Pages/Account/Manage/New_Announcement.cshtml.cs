using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Buy_Sell_Board.Data;
using Buy_Sell_Board.Models;
using Microsoft.AspNetCore.Authentication;
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
        public SelectList CategoryActionsList { get; set; }// категория выпадающий список
        public SelectList SubcategoryActionsList { get; set; }// подкатегория выпадающий список


        // коструктор со всем добром которое выше
        public New_AnnouncementModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ApplicationDbContext db,
            ILogger<ChangePasswordModel> logger)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            Input = new InputModel();// обязательно выделять память для выпадающих списков
            // конструктор выпадающих списков (Коллекция откуда брать, Имя параметра Ключ, Значение, Куда совать выбраный value)
            CategoryActionsList = new SelectList(_db.Categorys, "Id", "Name", Input.CategorySelectedValue);



        }


        [BindProperty] // флаг котороый говорт что это свойство модель биндиться к вью
        public InputModel Input { get; set; } // сам проп бинда 
        public string ReturnUrl { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        [TempData]  //хз
        public string StatusMessage { get; set; }

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

        }

        public async Task OnGetAsync(string returnUrl = null)
        {

            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

        }
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            var q = Input;
            return Page();
        }

        [HttpPost] // RequestVerificationToken
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
    }
}

