using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_FinalProject.DataAccess;

namespace PRN221_FinalProject.Pages.Home
{
    public class ViewOrderModel : PageModel
    {
        private readonly Prn221FinalProjectContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ViewOrderModel(Prn221FinalProjectContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<DataAccess.Order> Orders { get; set; }

        public int? accountId { get; set; }
        public void OnGet()
        {
            accountId = _httpContextAccessor.HttpContext.Session.GetInt32("AccountId");
            Orders = _context.Orders.Where(c => c.CustomerId == accountId).Include(m => m.Customer).ToList();
        }
    }
}
