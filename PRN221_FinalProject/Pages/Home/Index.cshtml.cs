using PRN221_FinalProject.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using PRN221_FinalProject.Pages.Login;
using Microsoft.AspNetCore.Http;
using PRN221_FinalProject.Session;

namespace PRN221_FinalProject.Pages.Home
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

        public List<Product> Products { get; set; }

        public void OnGet(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                Products = _context.Products
                    .Where(p => p.ProductName.Contains(search))
                    .ToList();
            }
            else
            {
                Products = _context.Products.ToList();
            }
        }

        public IActionResult OnPostAddToCart(int productId)
        {
            // Get the current AccountId from the session
            int? accountId = _httpContextAccessor.HttpContext.Session.GetInt32("AccountId");

            // If the user is not logged in, redirect to login page
            if (accountId == null)
            {
                return RedirectToPage("/Login/Index");
            }

            var cartDb = _context.Carts.ToList();
            var CartItem = cartDb.FirstOrDefault(c => c.AccountId == accountId && c.ProductId == productId);
            if (CartItem != null)
            {
                CartItem.Quantity++;
            }
            else
            {
                Product itemProduct = _context.Products.Find(productId);

                _context.Carts.Add(new DataAccess.Cart
                {
                    ProductId = productId,
                    AccountId = (int)accountId,
                    Quantity = 1
                });
            }
            _context.SaveChanges();
            return RedirectToPage();
        }
    }
}