using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PlayPeople.Pages
{
    [Authorize(Roles = "reader")]
    public class ReaderModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
