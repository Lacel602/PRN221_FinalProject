using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Identity.Client;
using PRN221_FinalProject.DataAccess;
using PRN221_FinalProject.Pages.Login;
using PRN221_FinalProject.Session;

namespace PRN221_FinalProject.Pages.Cart
{
    public class IndexModel : PageModel
    {
        private readonly Prn221FinalProjectContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IndexModel(Prn221FinalProjectContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public List<DataAccess.Cart> Carts { get; set; }
        public List<DataAccess.Product> Products { get; set; }

        public int? accountId { get; set; }
        public void OnGet()
        {
            accountId = _httpContextAccessor.HttpContext.Session.GetInt32("AccountId");
            Carts = _context.Carts.Where(c => c.AccountId == accountId).Include(c => c.Product).ToList();
        }

        public IActionResult OnPostQuantityEdit(int productId, int quantityChange)
        {
            accountId = _httpContextAccessor.HttpContext.Session.GetInt32("AccountId");
            var cartItem = _context.Carts.Where(c => c.AccountId == accountId).FirstOrDefault(c => c.ProductId == productId);
            if (cartItem != null)
            {
                if (quantityChange == 1)
                {
                    cartItem.Quantity++;
                }
                else
                {
                    cartItem.Quantity--;
                }
                if (cartItem.Quantity == 0)
                {
                    removeCartItem(productId);
                }
                _context.SaveChanges();
            }
            else
            {
                string errorMessage = "Product not found!";
                TempData["ErrorMessUpdate"] = errorMessage;
            }
            return RedirectToPage();
        }

        public IActionResult OnPostRemoveFromCart(int productId) 
        {
            removeCartItem(productId);
            return RedirectToPage();
        }


        private void removeCartItem(int productId)
        {
            accountId = _httpContextAccessor.HttpContext.Session.GetInt32("AccountId");
            var cartItem = _context.Carts.Where(c => c.AccountId == accountId).FirstOrDefault(c => c.ProductId == productId);
            if (cartItem != null)
            {
                _context.Carts.Remove(cartItem);
                _context.SaveChanges();
            }
        }
    }
}
