using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_FinalProject.DataAccess;

namespace PRN221_FinalProject.Pages.Cart
{
    public class CreateOrderModel : PageModel
    {
        private readonly Prn221FinalProjectContext _context;
        public int? accountId { get; set; }
        [BindProperty]
        public OrderInputModel OrderInput { get; set; }
        public Account user { get; set; }
        public decimal? totalMoney { get; set; }
        public CreateOrderModel(Prn221FinalProjectContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            accountId = HttpContext.Session.GetInt32("AccountId");
            user = _context.Accounts.Find(accountId);
            //Caculate total money
            var Carts = _context.Carts.Where(c => c.AccountId == accountId).Include(c => c.Product).ToList();
            if (Carts != null)
            {
                totalMoney = 0;
                foreach (var item in Carts)
                {
                    totalMoney += item.Quantity * item.Product.UnitPrice;
                }
            }
            else
            {
                TempData["ErrorMess"] = "Nothing in your cart =(";
                RedirectToPage("/Cart/Index");
            }
        }
        public IActionResult OnPost(int accountId)
        {
            DateTime currentDate = DateTime.Now;

            

            var Carts = _context.Carts.Where(c => c.AccountId == accountId).Include(c => c.Product).ToList();
            totalMoney = 0;
            foreach (var item in Carts)
            {
                totalMoney += item.Quantity * item.Product.UnitPrice;
                int orderId = GetLastOrderId();
                orderId++;
                _context.OrderDetails.Add(new OrderDetail
                {
                    OrderId = orderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    TotalMoney = item.Quantity * item.Product.UnitPrice
                });
            }

            _context.Orders.Add(new Order
            {
                CustomerId = accountId,
                OrderDate = currentDate,
                ShipAddress = OrderInput.ShipAddress,
                Phone = OrderInput.Phone,
                TotalOrderMoney = totalMoney.ToString(),
                Status = "Pending",
            });

            var cartItemsToRemove = _context.Carts.Where(c => c.AccountId == accountId);

            _context.Carts.RemoveRange(cartItemsToRemove);

            _context.SaveChanges();

            return RedirectToPage("/Home/ViewOrder");
        }

        public class OrderInputModel
        {
            public string ShipAddress { get; set; }
            public string Phone { get; set; }
        }

        public int GetLastOrderId()
        {
            // Query the Orders table and order by ID in descending order to get the last record
            var lastOrder = _context.Orders.OrderByDescending(o => o.OrderId).FirstOrDefault();

            if (lastOrder != null)
            {
                // Return the ID of the last order
                return lastOrder.OrderId;
            }

            // Return a default value (e.g., -1) or handle the case when there are no orders in the table
            return -1;
        }
    }
}
