using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineBankingApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBankingApp.Pages.FundTransfers
{
    public class IndexModel : PageModel
    {
        private readonly OnlineBankingApp.Data.OnlineBankingAppContext _context;

        public IndexModel(OnlineBankingApp.Data.OnlineBankingAppContext context)
        {
            _context = context;
        }

        public IList<FundTransfer> FundTransfer { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        //public async Task OnGetAsync()
        //{
        //    IQueryable<FundTransfer> fundtransfer = _context.FundTransfer;

        //    if (!string.IsNullOrEmpty(SearchString))
        //    {
        //        fundtransfer = _context.FundTransfer.FromSqlInterpolated(
        //            $"Select * from FundTransfer Where Note Like {"%" + SearchString + "%"}");
        //    }

        //    FundTransfer = await fundtransfer.ToListAsync();
        //}

        public async Task OnGetAsync()
        {
            IQueryable<FundTransfer> fundtransfer = _context.FundTransfer;

            if (!string.IsNullOrEmpty(SearchString))
            {
                fundtransfer = _context.FundTransfer.Where(ft => ft.Note.Contains(SearchString));
            }

            FundTransfer = await fundtransfer.ToListAsync();
        }

    }
}
