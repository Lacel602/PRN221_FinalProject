using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_FinalProject.DataAccess;

namespace PRN221_FinalProject.Pages.Admin
{
    public class EditOrderModel : PageModel
    {
        private readonly Prn221FinalProjectContext _context;
        public List<ShipCompany> shipCompanies { get; set; }
        public Order Order { get; set; }

        [BindProperty]
        public Order OrderInput { get; set; }
        public EditOrderModel(Prn221FinalProjectContext context)
        {
            _context = context;
            OrderInput = new Order();
        }
        public IActionResult OnGet(int orderId)
        {
            var accountId = HttpContext.Session.GetInt32("AccountId");
            if (accountId != null)
            {
                var account = _context.Accounts.FirstOrDefault(a => a.AccountId == accountId);
                if (account.Type.Contains("Staff"))
                {
                    shipCompanies = _context.ShipCompanies.ToList();
                    Order = _context.Orders.Find(orderId);
                    if (Order == null)
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

        public IActionResult OnPostUpdate(int orderId)
        {
            Order = _context.Orders.Find(orderId);
            if (Order == null)
            {
                string errorMessage = "Order not found!";
                TempData["ErrorMessUpdate"] = errorMessage;
                return RedirectToPage();
            }
            else
            {
                Order.ShipDate = OrderInput.ShipDate;
                Order.ShipCompanyId = OrderInput.ShipCompanyId;
                Order.Status = OrderInput.Status;

                _context.SaveChanges();

                return RedirectToPage("/Admin/ManagerOrder");
            }
        }
    }
}
