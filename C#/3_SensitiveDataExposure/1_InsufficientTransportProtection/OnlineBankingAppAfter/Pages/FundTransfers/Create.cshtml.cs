using Microsoft.AspNetCore.Mvc;
using OnlineBankingApp.Models;
using System.Threading.Tasks;

namespace OnlineBankingApp.Pages.FundTransfers
{
    public class CreateModel : AccountPageModel
    {
        private readonly OnlineBankingApp.Data.OnlineBankingAppContext _context;

        public CreateModel(OnlineBankingApp.Data.OnlineBankingAppContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateAccountsDropDownList(_context);
            return Page();
        }

        [BindProperty]
        public FundTransfer FundTransfer { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                PopulateAccountsDropDownList(_context);
                return Page();
            }

            _context.FundTransfer.Add(FundTransfer);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
