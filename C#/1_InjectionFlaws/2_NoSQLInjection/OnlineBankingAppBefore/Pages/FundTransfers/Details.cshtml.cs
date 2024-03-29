using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineBankingApp.Models;
using System.Threading.Tasks;

namespace OnlineBankingApp.Pages.FundTransfers
{
    public class DetailsModel : PageModel
    {
        private readonly OnlineBankingApp.Data.OnlineBankingAppContext _context;

        public DetailsModel(OnlineBankingApp.Data.OnlineBankingAppContext context)
        {
            _context = context;
        }

        public FundTransfer FundTransfer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            FundTransfer = await _context.FundTransfer.FirstOrDefaultAsync(m => m.ID == id);

            if (FundTransfer == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
