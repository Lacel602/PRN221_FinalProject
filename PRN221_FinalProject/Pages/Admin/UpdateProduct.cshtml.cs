using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_FinalProject.DataAccess;

namespace PRN221_FinalProject.Pages.Admin
{
    public class UpdateProductModel : PageModel
    {
        private readonly Prn221FinalProjectContext _context;
        public List<Category> Categories { get; set; }
        public Product product { get; set; }
        [BindProperty]
        public ProductInputModel ProductInput { get; set; }
        public UpdateProductModel(Prn221FinalProjectContext context)
        {
            _context = context;
            ProductInput = new ProductInputModel();
        }
        public IActionResult OnGet(int productId)
        {
            var accountId = HttpContext.Session.GetInt32("AccountId");
            if (accountId != null)
            {
                var account = _context.Accounts.FirstOrDefault(a => a.AccountId == accountId);
                if (account.Type.Contains("Staff"))
                {
                    Categories = _context.Categories.ToList();
                    product = _context.Products.Find(productId);
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

        public IActionResult OnPostUpdate(int productId)
        {
            var product = _context.Products.Find(productId);
            if (product == null)
            {
                string errorMessage = "Product not found!";
                TempData["ErrorMessUpdate"] = errorMessage;
                return RedirectToPage();
            }
            else
            {
                product.ProductName = ProductInput.ProductName;
                product.CategoryId = ProductInput.CategoryId;
                product.UnitPrice = ProductInput.UnitPrice;
                product.ProductImage = ProductInput.ProductImage;
                product.Description = ProductInput.Description;
                product.QuantityInStock = ProductInput.QuantityInStock;

                _context.SaveChanges();

                return RedirectToPage("/Admin/ManageProduct");
            }
        }
    }
}
