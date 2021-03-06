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
using Projeto_Rumos.Areas.Identity.Pages.Account.UserData;
using Models;
using Projeto_Rumos.ApiConector;
using WebApiFrutaria.DataContext;
using Projeto_Rumos.Models;

namespace Projeto_Rumos.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ContextApplication _dbContext;
        private readonly ApiConnector _apiConnector;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ContextApplication dbContext,
            ApiConnector apiConnector)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _dbContext = dbContext;
            _apiConnector = apiConnector;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            public string UserName { get; set; }
            public string SobreNome { get; set; }
            public string Morada { get; set; }
            public DateTime DataNascimento { get; set; }
            public string PhoneNumber { get; set; }

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
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                try
                {
                    //REGISTO DE UM NOVO USUARIO NA CLASS USUARIO, PARA USO NO FINALIZAR DA ENCOMENDA.
                    Usuario Usuario = new Usuario
                    {
                        Username = Input.UserName,
                        Morada = Input.Morada,
                        DataNascimento = Input.DataNascimento,
                        Contacto = Input.PhoneNumber,
                        Email = Input.Email,
                    };

                    _apiConnector.Post("usuario", Usuario.ToString());
                    _dbContext.SaveChanges();

                    //AQUI É TAMBÉM REGISTADO UM USUSARIO MAS PARA A CLASS REGISTER, CRIADA AUTOMATICAMENTE PELO IDENTITY PARA EFEITOS
                    //DE LOGIN.
                    var user = new ApplicationUser { UserName = Input.UserName, Email = Input.Email, PhoneNumber = Input.PhoneNumber, Surname = Input.SobreNome, StreetAddress = Input.Morada, DateOfBirth = Input.DataNascimento };
                    var result = await _userManager.CreateAsync(user, Input.Password);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User created a new account with password.");

                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                            protocol: Request.Scheme);

                        await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                        if (_userManager.Options.SignIn.RequireConfirmedAccount)
                        {
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
                catch (Exception msg)
                {
                    ErrorViewModel errorViewModel = new ErrorViewModel();
                    errorViewModel.RequestId = msg.Message;

                    return RedirectToAction("_Error", errorViewModel);
                }
            }
            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
