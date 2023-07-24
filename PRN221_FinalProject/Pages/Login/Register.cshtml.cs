using PRN221_FinalProject.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PRN221_FinalProject.Pages.Login
{
    public class RegisterModel : PageModel
    {
        private readonly Prn221FinalProjectContext _context;

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string FullName { get; set; }


        public RegisterModel(Prn221FinalProjectContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            // Perform registration logic here
            // Example: Save the entered user information to the database
            List<Account> Accounts = _context.Accounts.ToList();
            if (checkAccount(Username, Accounts))
            {         
                var account = new Account
                {
                    Username = Username,
                    Password = Password,
                    FullName = FullName,
                    Type = "Customer"
                };

                _context.Accounts.Add(account);
                _context.SaveChanges();
                return RedirectToPage("/Login/Index");
            }
            else
            {
                string errorMessage = "Username exists!";
                TempData["ErrorMess"] = errorMessage;
                return RedirectToPage("/Login/Register");
            }                     
        }

        private bool checkAccount(string username, List<Account> accounts)
        {
            foreach (var account in accounts)
            {
                if(account.Username == username)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
