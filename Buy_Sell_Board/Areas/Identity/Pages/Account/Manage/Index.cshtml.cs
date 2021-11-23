using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Buy_Sell_Board.Data;
using Buy_Sell_Board.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Buy_Sell_Board.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _db;

        public IndexModel(  ApplicationDbContext db,
        UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }
       
        private async Task LoadAsync(AppUser user)
        {
            Input = new InputModel
            {
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Date_of_Birth = user.Date_of_Birth,
                City = user.City,
                Region = user.Region,            
            };
        }
        public async Task<IActionResult> OnGetAsync()
        {
            
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // получае юзера
             var user = await _userManager.GetUserAsync(User);
            // проверяем что получили
            if (user == null)
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            
            // если модель не валидна перезашрузись
            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }
            //меняем пропы
            user.PhoneNumber = Input.PhoneNumber;
            user.FirstName = Input.FirstName;
            user.LastName = Input.LastName;
            user.Date_of_Birth = Input.Date_of_Birth;
            user.City = Input.City;
            user.Region = Input.Region;
            
            // сохраняем изминение
            await _userManager.UpdateAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
        public class InputModel
        {
            // кастомное поле
            [Required]
            [DataType(DataType.Text)]// тип данных кастомного поля 
            [Display(Name = "First Name")]// название на view
            public string FirstName { get; set; }

            // кастомное поле
            [Required]
            [DataType(DataType.Text)]// тип данных кастомного поля 
            [Display(Name = "Last Name")]// название на view
            public string LastName { get; set; }
            // кастомное поле
            [Required]
            [DataType(DataType.Date)]// тип данных кастомного поля 
            [Display(Name = "Date of Birth")]// название на view
            public DateTime Date_of_Birth { get; set; }

            // кастомное поле
            [Required]
            [DataType(DataType.Text)]// тип данных кастомного поля 
            [Display(Name = "City")]// название на view
            public string City { get; set; }

            // кастомное поле
            [Required]
            [DataType(DataType.Text)]// тип данных кастомного поля 
            [Display(Name = "Region")]// название на view
            public string Region { get; set; }

            // кастомное поле
            [Required]
            [DataType(DataType.PhoneNumber)]// тип данных кастомного поля 
            [Display(Name = "PhoneNumber")]// название на view
            public string PhoneNumber { get; set; }
        }
    }
}
