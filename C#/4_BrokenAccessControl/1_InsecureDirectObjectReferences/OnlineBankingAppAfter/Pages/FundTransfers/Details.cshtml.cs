using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineBankingApp.Data;
using OnlineBankingApp.Models;

namespace OnlineBankingApp.Pages.FundTransfers
{
    public class DetailsModel : PageModel
    {
        private readonly OnlineBankingApp.Data.OnlineBankingAppContext _context;

        protected IAuthorizationService _authorizationService { get; }
        public DetailsModel(OnlineBankingApp.Data.OnlineBankingAppContext context,
                            IAuthorizationService authorizationService)
        {
            _context = context;
            _authorizationService = authorizationService;
        }

        public FundTransfer fundTransfer { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            if (!User.Identity.IsAuthenticated)
            {
                return Challenge();
            }

            fundTransfer = await _context.FundTransfer
                                .Where(f => f.ID == id)
                                .Include(f => f.Customer)
                                .OrderBy(f => f.TransactionDate)
                                .FirstOrDefaultAsync<FundTransfer>();

            var isAuthorized = await _authorizationService.
                AuthorizeAsync( User, fundTransfer, "Owner");

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            return Page();
        }
    }
}