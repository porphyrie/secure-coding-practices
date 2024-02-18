using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineBankingApp.Data;
using System.Linq;

namespace OnlineBankingApp.Pages.FundTransfers
{
    public class AccountPageModel : PageModel
    {
        public SelectList AccountSL { get; set; }

        public void PopulateAccountsDropDownList(OnlineBankingAppContext _context)
        {
            ViewData["CustomerID"] = "1";
            var accountsQuery = _context.Account
                .Where(a => a.Customer.Id == (string)ViewData["CustomerID"])
                .OrderBy(a => a.ID);

            AccountSL = new SelectList(accountsQuery.AsNoTracking(), "ID", "Name");
        }
    }
}