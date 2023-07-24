using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_FinalProject.DataAccess;

namespace PRN221_FinalProject.Pages.Admin
{
    public class ManagerOrderModel : PageModel
    {
        private readonly Prn221FinalProjectContext _context;

        public ManagerOrderModel(Prn221FinalProjectContext context)
        {
            _context = context;
        }

        public List<Order> Orders { get; set; }
        public IActionResult OnGet()
        {
            var accountId = HttpContext.Session.GetInt32("AccountId");
            if (accountId != null)
            {
                var account = _context.Accounts.FirstOrDefault(a => a.AccountId == accountId);
                if (account.Type.Contains("Staff"))
                {
                    Orders = _context.Orders.Include(m => m.Customer).ToList();
                    return Page();
                }
                else
                {
                    string Message = "Your account is not permitted to edit!";
                    TempData["Mess"] = Message;
                    return RedirectToPage("/login/index");
                }
            }
            else
            {
                string Message = "Your account is not permitted to edit!";
                TempData["Mess"] = Message;
                return RedirectToPage("/login/index");
            }
        }
    }
}
