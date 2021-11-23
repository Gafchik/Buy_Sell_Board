using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Buy_Sell_Board.Controllers;
using Buy_Sell_Board.Data;
using Buy_Sell_Board.Models;
using Buy_Sell_Board.Models.Announcement;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Buy_Sell_Board.Areas.Identity.Pages.Account.Manage
{
    public class New_AnnouncementModel : PageModel
    {
        // логеры и менеджеры           
        private readonly SignInManager<AppUser> _signInManager;       
        public ApplicationDbContext _db;       
        // коструктор со всем добром которое выше
        public New_AnnouncementModel(SignInManager<AppUser> signInManager,ApplicationDbContext db,IWebHostEnvironment appEnvironment)
        {          
            _db = db;             
            _signInManager = signInManager;   
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

