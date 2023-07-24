using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_FinalProject.DataAccess;
using System.ComponentModel.DataAnnotations;

namespace PRN221_FinalProject.Pages.Admin
{
    public class ManageAccountModel : PageModel
    {
        private readonly Prn221FinalProjectContext _context;

        public AccountTypeInputModel inputModel { get; set; }
        public List<Account> Accounts { get; set; }

        public ManageAccountModel(Prn221FinalProjectContext context)
        {
            _context = context;
            inputModel = new AccountTypeInputModel();
        }

        public void OnGet()
        {
            Accounts = _context.Accounts.ToList();
        }

        public IActionResult OnPostDelete(int accountId)
        {
            if (accountId != 1)
            {
                var account = _context.Accounts.Find(accountId);
                if (account != null)
                {
                    _context.Accounts.Remove(account);
                    _context.SaveChanges();
                    TempData["Mess"] = "Account deleted successfully.";
                }
                else
                {
                    TempData["ErrorMess"] = "Account not found.";
                }
            }
            else
            {
                TempData["ErrorMess"] = "This is admin account, cannot delete!!!";
            }
            return RedirectToPage();
        }

        public IActionResult OnPostUpdateType(int accountId, AccountTypeInputModel inputModel)
        {
            var account = _context.Accounts.Find(accountId);
            if (accountId != 1)
            {
                if (account != null)
                {
                    // Update the account type
                    account.Type = inputModel.Type;
                    _context.SaveChanges();
                    TempData["Mess"] = "Account type updated successfully.";
                }
                else
                {
                    TempData["ErrorMess"] = "Account not found.";
                }
            }
            else
            {
                TempData["ErrorMess"] = "This is admin account, cannot change type!!!";
            }

            return RedirectToPage();
        }
    }

    public class AccountTypeInputModel
    {
        [Required]
        public string Type { get; set; }
    }
}