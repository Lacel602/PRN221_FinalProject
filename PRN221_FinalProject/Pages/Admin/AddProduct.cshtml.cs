using PRN221_FinalProject.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PRN221_FinalProject.Pages.Admin
{
    public class AddProductModel : PageModel
    {
        private readonly Prn221FinalProjectContext _context;
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        [BindProperty]
        public ProductInputModel ProductInput { get; set; }
        public AddProductModel(Prn221FinalProjectContext context)
        {
            _context = context;
            ProductInput = new ProductInputModel();
        }
        public void OnGet()
        {
            Categories=_context.Categories.ToList();
        }

        public IActionResult OnPostAdd(ProductInputModel productInput)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = "Some field is not valid!";
                TempData["ErrorMessAdd"] = errorMessage;
                return RedirectToPage();
            }
            else
            {
                Products = _context.Products.ToList();
                int lastId = Products.Max(p => p.ProductId);
                lastId++;
                var product = new Product
                {
                    ProductId = lastId,
                    ProductName = productInput.ProductName,
                    CategoryId = productInput.CategoryId,
                    UnitPrice = productInput.UnitPrice,
                    ProductImage = productInput.ProductImage,
                    Description = productInput.Description,
                    QuantityInStock = productInput.QuantityInStock,
                };

                _context.Products.Add(product);
                _context.SaveChanges();

                string Message = "Add new product Succesfully";
                TempData["Mess"] = Message;

                return RedirectToPage("/Admin/ManageProduct");
            }

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
}
