using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineBankingApp.Models;
using OnlineBankingApp.Services;
using System.Threading.Tasks;

namespace OnlineBankingApp.Pages.Payees
{
    public class CreateModel : PageModel
    {
        private readonly IPayeeService _payeeService;

        public CreateModel(IPayeeService payeeService)
        {
            _payeeService = payeeService;
        }

        [BindProperty]
        public Payee Payee { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _payeeService.Create(Payee);

            return RedirectToPage("./Index");
        }
    }
}
