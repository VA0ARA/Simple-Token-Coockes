using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace One.Pages.Account
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnPostAsync()
        {
            // kill coockes
            await HttpContext.SignOutAsync("MyCookieAuth");
            return RedirectToPage("/Index");
        }
    }
}
