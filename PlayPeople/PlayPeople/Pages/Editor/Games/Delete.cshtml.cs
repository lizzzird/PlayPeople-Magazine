using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlayPeople.Services;

namespace PlayPeople.Pages.Editor.Games
{
    public class DeleteModel : PageModel
    {
		private readonly IWebHostEnvironment environment;
		private readonly ApplicationDbContext context;

		public DeleteModel(IWebHostEnvironment environment, ApplicationDbContext context)
        {
			this.environment = environment;
			this.context = context;
		}
        public void OnGet(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/Editor/Games/Index");
                return;
            }

            var game = context.Games.Find(id);
            if(game == null)
            {
                Response.Redirect("/Editor/Games/Index");
                return;
            }

            string imageFullPath = environment.WebRootPath + "/games" + game.ImageFileName;
            System.IO.File.Delete(imageFullPath);

            context.Games.Remove(game);
            context.SaveChanges();

            Response.Redirect("/Editor/Games/Index");
        }
    }
}
