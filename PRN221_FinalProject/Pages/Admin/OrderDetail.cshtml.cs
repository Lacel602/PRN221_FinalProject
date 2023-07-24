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

        public List<ShipCompany> ShipCompanies { get; set; }
        [BindProperty]
        public Order Order { get; set; }
        [BindProperty]
        public OrderDetail Details { get; set; }
        public int orderId { get; set; }

        public IActionResult OnGet(int id)
        {
            var accountId = HttpContext.Session.GetInt32("AccountId");
            if (accountId != null)
            {
                var account = _context.Accounts.FirstOrDefault(a => a.AccountId == accountId);
                if (account.Type.Contains("Staff"))
                {
                    orderId = id;
                    Order = new PRN221_FinalProject.DataAccess.Order();
                    Details = new PRN221_FinalProject.DataAccess.OrderDetail();
                    var orderdetails = _context.OrderDetails.ToList().SingleOrDefault(o => o.OrderId == id);
                    var order = _context.Orders.Include(m => m.Customer).ToList().FirstOrDefault(o => o.OrderId == id);
                    Order.Customer.FullName = order.Customer.FullName;
                    Order.OrderDate = order.OrderDate;
                    Order.ShipAddress = order.ShipAddress;
                    Order.Phone = order.Phone;
                    Details.Quantity = orderdetails.Quantity;

                    ShipCompanies = _context.ShipCompanies.ToList();
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
