using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_FinalProject.DataAccess;

namespace PRN221_FinalProject.Pages.Login
{
    public class UserProfileModel : PageModel
    {
        private readonly Prn221FinalProjectContext _context;
        public int? accountId { get; set; }
        public Account user { get; set; }
        [BindProperty]
        public UserInputModel UserInput { get; set; }
        public UserProfileModel(Prn221FinalProjectContext context)
        {
            _context = context;
            UserInput = new UserInputModel();
        }
        public void OnGet()
        {
            accountId = HttpContext.Session.GetInt32("AccountId");
            if (accountId != null)
            {
                user = _context.Accounts.FirstOrDefault(a => a.AccountId == accountId);
                string a = user.FullName;
            }
            else
            {
                // Handle the case when no user is logged in or accountId is invalid
                // For example, you might redirect the user to the login page or display an error message
            }
        }

        public IActionResult OnPostProfileUpdate()
        {
            accountId = HttpContext.Session.GetInt32("AccountId");
            user = _context.Accounts.Find(accountId);
            string a = user.FullName;
            if (user != null)
            {
                user.Username = UserInput.Username;
                user.FullName = UserInput.Fullname;
                user.Address = UserInput.Address;
                user.Phone = UserInput.Phone;
                user.Email = UserInput.Email;
                _context.SaveChanges();
                string errorMessage = "Profile updated!";
                TempData["SuccessMess"] = errorMessage;
                return RedirectToPage("/Login/UserProfile");
            }
            else
            {
                
                string errorMessage = "User model not valid!";
                TempData["ErrorMessUpdate"] = errorMessage;
                return RedirectToPage();
            }
        }

        public class UserInputModel
        {
            public string Username { get; set; }
            public string Fullname { get; set; }
            public string Address { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
        }
    }
}