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

        public void PopulateAccountsDropDownList(OnlineBankingAppContext _context, string customerId)
        {
            var accountsQuery = _context.Account.Where(a => a.Customer.Id == customerId).OrderBy(a => a.ID);
            AccountSL = new SelectList(accountsQuery.AsNoTracking(), "ID", "Name");
        }
    }
}