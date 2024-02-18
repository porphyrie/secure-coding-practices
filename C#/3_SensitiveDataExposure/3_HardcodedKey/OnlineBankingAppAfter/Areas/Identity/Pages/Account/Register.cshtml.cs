using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using OnlineBankingApp.Models;
using OnlineBankingApp.Services;

namespace OnlineBankingApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<Customer> _signInManager;
        private readonly UserManager<Customer> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IConfiguration _configuration;
        private readonly ICryptoService _cryptoService;

        //public RegisterModel(
        //    UserManager<Customer> userManager,
        //    SignInManager<Customer> signInManager,
        //    ILogger<RegisterModel> logger,
        //    IConfiguration configuration,
        //    ICryptoService cryptoService)
        //{
        //    _userManager = userManager;
        //    _signInManager = signInManager;
        //    _logger = logger;
        //    _configuration = configuration;
        //    _cryptoService = cryptoService;
        //}

        public RegisterModel(
            UserManager<Customer> userManager,
            SignInManager<Customer> signInManager,
            ILogger<RegisterModel> logger,
            ICryptoService cryptoService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _cryptoService = cryptoService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
            [Display(Name = "First Name")]
            [StringLength(60, MinimumLength = 3)]
            [Required]        
            public string FirstName { get; set; }

            [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
            [Display(Name = "Middle Name")]
            [StringLength(60, MinimumLength = 3)]
            [Required]        
            public string MiddleName { get; set; }

            [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
            [Display(Name = "Last Name")]
            [StringLength(60, MinimumLength = 3)]
            [Required]        
            public string LastName { get; set; }

            [Required]
            [DisplayFormat(DataFormatString = "{mm/dd/yyyy}")]
            [DataType(DataType.Date)]
            public DateTime DateOfBirth { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new Customer
                {
                    FirstName = _cryptoService.Encrypt(
                        Input.FirstName,
                        Environment.GetEnvironmentVariable("DbDataKey", EnvironmentVariableTarget.Machine)),
                    MiddleName = _cryptoService.Encrypt(
                        Input.MiddleName,
                        Environment.GetEnvironmentVariable("DbDataKey", EnvironmentVariableTarget.Machine)),
                    LastName = _cryptoService.Encrypt(
                        Input.LastName,
                        Environment.GetEnvironmentVariable("DbDataKey", EnvironmentVariableTarget.Machine)),
                    DateOfBirth = Input.DateOfBirth,
                    UserName = Input.Email,
                    Email = Input.Email
                };

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                            protocol: Request.Scheme);

                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
