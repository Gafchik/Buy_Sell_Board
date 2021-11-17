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
        // ������ � ���������
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<ChangePasswordModel> _logger;
        public ApplicationDbContext _db;
       /* public SelectList CategoryActionsList { get; set; }// ��������� ���������� ������
        public SelectList SubcategoryActionsList { get; set; }// ������������ ���������� ������*/
        private readonly IWebHostEnvironment _appEnvironment; // ��������� ��� ���������


        // ���������� �� ���� ������ ������� ����
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
            /*Input = new InputModel();// ����������� �������� ������ ��� ���������� �������
            // ����������� ���������� ������� (��������� ������ �����, ��� ��������� ����, ��������, ���� ������ �������� value)
            CategoryActionsList = new SelectList(_db.Categorys, "Id", "Name", Input.CategorySelectedValue);*/
        }


        [BindProperty] // ���� �������� ������ ��� ��� �������� ������ ��������� � ���
        public InputModel Input { get; set; } // ��� ���� ����� 
        public string ReturnUrl { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        [TempData]  //��
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
                    // ���� � ����� Image
                    string path = "/Image/" + img.FileName;
                    // ��������� ���� � ����� Files � �������� wwwroot
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                        await img.CopyToAsync(fileStream);
                    // ~ ��� ������� � ��� ��� �� �������� � ����� wwwroot
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
        #region ���� �������
        [HttpPost] // ��������� ������������� ��� ������ ��� ������� � �������  �����
        public JsonResult OnPostFirstCat([FromBody] JsonElement json) => new JsonResult(_db.Categorys.ToList());

        [HttpPost] // ��������� ������������� ��� ������ ��� ������� � �������  �����
        public JsonResult OnPostFirstSubCat([FromBody] JsonElement json) => new JsonResult(_db.Subcategorys.ToList().FindAll(i => i.Category_Id == 1));

        [HttpPost] // RequestVerificationToken ����� ���������� ������������
        public JsonResult OnPostSubCut([FromBody] JsonElement json)
        {
            // ������ �������� json � ������
            var jsonStr = System.Text.Json.JsonSerializer.Deserialize<object>(json.GetRawText()).ToString();
            //������ ������ � �������
            dynamic data = JObject.Parse(jsonStr);
            // � ������� � ������ ��� ������
            var SellCat = int.Parse(data.SelectedValue.Value);
            // ������ ������� � �� � � json ����������
            return new JsonResult(_db.Subcategorys.ToList().FindAll(i => i.Category_Id == SellCat));
        }
        #endregion
        // ����� ����� ����� ������
        public class InputModel
        {

            [Required]
            [Display(Name = "Category")]
            public int CategorySelectedValue { get; set; }   // ��������� ����  

            [Required]
            [Display(Name = "Subcategory")]
            public int SubcategorySelectedValue { get; set; }   // ������������ ����  

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
            public IFormFileCollection Files { get; set; } // ����  ��������� ���������� ������
        }
    }
}

