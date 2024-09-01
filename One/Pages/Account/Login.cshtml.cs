using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using One.Model;
using System.Security.Claims;

namespace One.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential Credential { get; set; } = new Credential();
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            // Verify the credential
            if (Credential.UserName == "admin" && Credential.Password == "password")
            {
                // Creating the security context
                //1.set Items to access
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.Email, "admin@mywebsite.com"),
                    new Claim("Department", "HR"),
                    new Claim("Admin", "true"),
                    new Claim("Manager", "true"),
                   new Claim("EmploymentDate", "2024-10-01")
                };
                //2.create identity 
                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                //3.ClaimsPrincipal is a class that represents
                //the current user and their associated identities.
                //A ClaimsPrincipal can contain
                //multiple ClaimsIdentity objects (e.g., one for basic
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
                //4.encapsulate various settings and properties that control the
                //behavior of the authentication proces-RedirectUri-IsPersistent-AllowRefresh-ExpiresUtc
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = Credential.RememberMe
                };
                //create coockes- is a method in ASP.NET Core that is used
                //to sign in a user by issuing an
                //authentication cookie or other forms of authentication tokens
                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal, authProperties);
                return RedirectToPage("/Index");
            }
            return Page();
        }
    }
}
