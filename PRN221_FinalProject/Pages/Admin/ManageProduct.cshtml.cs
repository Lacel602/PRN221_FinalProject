using System.Collections.Generic;
using System.Linq;
using PRN221_FinalProject.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace PRN221_FinalProject.Pages.Admin
{
    public class ManageProductsModel : PageModel
    {
        private readonly Prn221FinalProjectContext _context;

        public ManageProductsModel(Prn221FinalProjectContext context)
        {
            _context = context;
            ProductInput = new ProductInputModel();
        }
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }

        [BindProperty]
        public ProductInputModel ProductInput { get; set; }

        public IActionResult OnGet()
        {
            var accountId = HttpContext.Session.GetInt32("AccountId");
            if (accountId != null)
            {
                var account = _context.Accounts.FirstOrDefault(a => a.AccountId == accountId);
                if (account.Type.Contains("Staff"))
                {
                    Products = _context.Products.ToList();
                    Categories = _context.Categories.ToList();
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

        public IActionResult OnPostDelete(int productId)
        {
            var product = _context.Products.Find(productId);
            if (product == null)
            {
                string errorMessage = "Product not found!";
                TempData["ErrorMessDelete"] = errorMessage;
                return RedirectToPage();
            }
            else
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
                string Message = "Delete details Succesfully";
                TempData["Mess"] = Message;
                return RedirectToPage();
            }
        }

        //public IActionResult OnPostAdd(ProductInputModel productInput)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        string errorMessage = "Some field is not valid!";
        //        TempData["ErrorMessAdd"] = errorMessage;
        //        return RedirectToPage();
        //    }
        //    else
        //    {
        //        Products = _context.Products.ToList();
        //        int lastId = Products.Max(p => p.ProductId);
        //        lastId++;
        //        var details = new Product
        //        {
        //            ProductId = lastId,
        //            ProductName = productInput.ProductName,
        //            CategoryId = productInput.CategoryId,
        //            UnitPrice = productInput.UnitPrice,
        //            ProductImage = productInput.ProductImage
        //        };

        //        _context.Products.Add(details);
        //        _context.SaveChanges();

        //        string Message = "Add new details Succesfully";
        //        TempData["Mess"] = Message;

        //        return RedirectToPage("/Admin/ManageProduct");
        //    }

        //}

        public IActionResult OnPostUpdate(int productId)
        {
            return RedirectToPage("/Admin/UpdateProduct", new { productId });
        }

        //public IActionResult OnPostUpdate(int productId)
        //{
        //    var details = _context.Products.Find(productId);
        //    if (details == null)
        //    {
        //        string errorMessage = "Product not found!";
        //        TempData["ErrorMessUpdate"] = errorMessage;
        //        return RedirectToPage();
        //    }
        //    else
        //    {
        //        details.ProductName = ProductInput.ProductName;
        //        details.CategoryId = ProductInput.CategoryId;
        //        details.UnitPrice = ProductInput.UnitPrice;
        //        details.ProductImage = ProductInput.ProductImage;

        //        _context.SaveChanges();

        //        return RedirectToPage();
        //    }
        //}
    }

    public class ProductInputModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int QuantityInStock { get; set; }
        public int CategoryId { get; set; }
        public decimal UnitPrice { get; set; }
        public string ProductImage { get; set; }
        public string Description { get; set; }
    }
}