using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Buy_Sell_Board.Areas.Identity.Pages.Account.Manage
{
    public static class ManageNavPages
    {
        public static string New_Announcement => "New Announcement"; //проп имени для вью
        public static string New_AnnouncementNavClass(ViewContext viewContext) => PageNavClass(viewContext, New_Announcement);//  метод для перехода на страницу


        public static string Index => "Index"; //проп имени для вью
        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);//  метод для перехода на страницу

        public static string Email => "Email"; //проп имени для вью
        public static string EmailNavClass(ViewContext viewContext) => PageNavClass(viewContext, Email);//  метод для перехода на страницу

        public static string ChangePassword => "ChangePassword"; //проп имени для вью
        public static string ChangePasswordNavClass(ViewContext viewContext) => PageNavClass(viewContext, ChangePassword);//  метод для перехода на страницу

        public static string DownloadPersonalData => "DownloadPersonalData"; //проп имени для вью
        public static string DownloadPersonalDataNavClass(ViewContext viewContext) => PageNavClass(viewContext, DownloadPersonalData);//  метод для перехода на страницу

        public static string DeletePersonalData => "DeletePersonalData"; //проп имени для вью
        public static string DeletePersonalDataNavClass(ViewContext viewContext) => PageNavClass(viewContext, DeletePersonalData);//  метод для перехода на страницу

        public static string ExternalLogins => "ExternalLogins"; //проп имени для вью
        public static string ExternalLoginsNavClass(ViewContext viewContext) => PageNavClass(viewContext, ExternalLogins);//  метод для перехода на страницу

        public static string PersonalData => "PersonalData"; //проп имени для вью
        public static string PersonalDataNavClass(ViewContext viewContext) => PageNavClass(viewContext, PersonalData);//  метод для перехода на страницу

        public static string TwoFactorAuthentication => "TwoFactorAuthentication"; //проп имени для вью
        public static string TwoFactorAuthenticationNavClass(ViewContext viewContext) => PageNavClass(viewContext, TwoFactorAuthentication);//  метод для перехода на страницу
        // нахрен это  я хз 
        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
