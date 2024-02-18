using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineBankingApp.Data;
using OnlineBankingApp.Models;

using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using OnlineBankingApp.Authorization;

namespace OnlineBankingApp.Pages.FundTransfers
{
    [Authorize(Policy = nameof(PrincipalPermission.CanCreateFundTransfer))]
    public class CreateModel : AccountPageModel
    {
        private readonly OnlineBankingApp.Data.OnlineBankingAppContext _context;

        public CreateModel(OnlineBankingApp.Data.OnlineBankingAppContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var loggedInUser = HttpContext.User;
            var customerId = loggedInUser.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            PopulateAccountsDropDownList(_context, customerId);
            return Page();
        }

        [BindProperty]
        public FundTransfer FundTransfer { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var loggedInUser = HttpContext.User;
            var customerId = loggedInUser.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

            var emptyFundTransfer = new FundTransfer();
            emptyFundTransfer.CustomerID = customerId;

            if (await TryUpdateModelAsync<FundTransfer>(
                 emptyFundTransfer,
                 "fundtransfer",
                 f => f.ID, f => f.AccountFrom, f => f.AccountTo, f => f.Amount, f => f.TransactionDate, f => f.Note, f => f.CustomerID))
            {
                _context.FundTransfer.Add(emptyFundTransfer);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
