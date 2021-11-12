using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Buy_Sell_Board.Data;
using Buy_Sell_Board.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace Buy_Sell_Board.Areas.Identity.Pages.Account.Manage
{
    public class New_AnnouncementModel : PageModel
    {
        // логеры и менеджеры
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<ChangePasswordModel> _logger;
        public ApplicationDbContext _db;
        public List<SelectListItem> CategoryActionsList { get; set; }// категория выпадающий список

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
            CategoryActionsList = new List<SelectListItem>();
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
                   
          
            public int Subcategory_Id { get; set; } // подкатегория айди
            [Required]
            [DataType(DataType.Text)]// тип данных кастомного поля 
            [Display(Name = "Category")]
            public string Category_Id { get; set; }     // категория айди   


            

            //[Required]
            //[Display(Name = "Subcategory")]
            //public List<SelectListItem> SubcategoryActionsList // категория выпадающий список
            //{
            //    get 
            //    {

            //        if (SubcategoryActionsList != null)// если лист уже создан то сингл тон
            //            SubcategoryActionsList.Clear();
            //        else // если не создан то выдиляем память
            //            SubcategoryActionsList = new List<SelectListItem>();
            //        //заполняем значениями с БД
            //        ApplicationDbContext.Subcategorys.Select(i => i.Category_Id == CategoryActionsList.).ToList()
            //    .ForEach(i => SubcategoryActionsList.Add(new SelectListItem
            //    { Text = i.Name, Value = i.Id.ToString() }));
            //    }
            //    set { CategoryActionsList = value; }
            //}


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
            _db.Categorys.ToList().ForEach(i => CategoryActionsList.Add(new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }));


        }
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            var q = Input;
            return Page();
        }
    }
}

