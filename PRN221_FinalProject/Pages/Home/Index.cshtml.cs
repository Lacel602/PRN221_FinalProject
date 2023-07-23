using PRN221_FinalProject.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PRN221_FinalProject.Pages.Home
{
    public class IndexModel : PageModel
    {
        private readonly Prn221FinalProjectContext _context;

        public IndexModel(Prn221FinalProjectContext context)
        {
            _context = context;
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
    }
}