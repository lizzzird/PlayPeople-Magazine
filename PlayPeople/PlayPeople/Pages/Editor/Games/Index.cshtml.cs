using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlayPeople.Models;
using PlayPeople.Services;

namespace PlayPeople.Pages.Editor.Games
{
    public class IndexModel : PageModel
    {
		public ApplicationDbContext context { get; }

		public List<Game> Games { get; set; } = new List<Game>();
		public IndexModel(ApplicationDbContext context)
		{
			this.context = context;
		}


		public void OnGet()
		{
			Games = context.Games.OrderByDescending(g => g.Id).ToList();
		}
	}
}
