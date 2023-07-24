using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_FinalProject.DataAccess;

namespace PRN221_FinalProject.Pages.Admin
{
    public class OrderDetailModel : PageModel
    {
        private readonly Prn221FinalProjectContext _context;

        public OrderDetailModel(Prn221FinalProjectContext context)
        {
            _context = context;
        }
        public int id { get; set; }
        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        public IActionResult OnGet(int orderId)
        {
            var accountId = HttpContext.Session.GetInt32("AccountId");
            if (accountId != null)
            {
                var account = _context.Accounts.FirstOrDefault(a => a.AccountId == accountId);
                if (account.Type.Contains("Staff"))
                {
                    OrderDetails = _context.OrderDetails.Include(m => m.Product).Where(x => x.OrderId == orderId).ToList();

                    if (OrderDetails == null)
                    {
                        TempData["ErrorMessUpdate"] = "Product not found!";
                        return RedirectToPage("/Admin/ManagerOrder");
                    }
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
                string Message = "You need to login to use this!";
                TempData["Mess"] = Message;
                return RedirectToPage("/login/index");
            }
        }
    }
}
