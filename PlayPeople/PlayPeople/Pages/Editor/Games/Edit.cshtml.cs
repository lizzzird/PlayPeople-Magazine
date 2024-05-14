using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlayPeople.Models;
using PlayPeople.Services;

namespace PlayPeople.Pages.Editor.Games
{
    public class EditModel : PageModel
    {
		private readonly IWebHostEnvironment environment;
		private readonly ApplicationDbContext context;

        [BindProperty]  
        public GameDto GameDto { get; set; } = new GameDto();

        public Game Game {  get; set; } = new Game();

        public string errorMessage = "";
        public string successMessage = "";

		public EditModel(IWebHostEnvironment environment, ApplicationDbContext context)
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
            if (game == null)
            {
                Response.Redirect("/Editor/Games/Index");
                return;
            }

            GameDto.Title = game.Title;
            GameDto.Developer = game.Developer;
            GameDto.Genre = game.Genre;
            GameDto.Description = game.Description;
            GameDto.ReleaseDate = game.ReleaseDate;
            GameDto.Platforms = game.Platforms;

            Game = game;
        }
        public void OnPost(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/Editor/Games/Index");
                return;
            }

            if(!ModelState.IsValid)
            {
                errorMessage = "Пожалуйста, заполните все необходимые поля";
                return;
            }

            var game = context.Games.Find(id);
            if (game == null)
            {
                Response.Redirect("/Editor/Games/Index");
                return ;
            }

            string newFileName = game.ImageFileName;
            if(GameDto.ImageFile != null)
            {
                newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newFileName += Path.GetExtension(GameDto.ImageFile.FileName);

                string imageFullPath = environment.WebRootPath + "/games/" + newFileName;
                using (var stream = System.IO.File.Create(imageFullPath))
                {
                    GameDto.ImageFile.CopyTo(stream);
                }

                string oldImageFullPath = environment.WebRootPath + "/games/" + game.ImageFileName;
                System.IO.File.Delete(oldImageFullPath);
            }

            game.Title = GameDto.Title;
            game.Developer = GameDto.Developer;
			game.Genre = GameDto.Genre;
			game.Description = GameDto.Description ?? "";
			game.ReleaseDate = GameDto.ReleaseDate;
			game.Platforms = GameDto.Platforms;
            game.ImageFileName = newFileName;

            context.SaveChanges();

			successMessage = "Игра успешно отредактирована";

            Response.Redirect("/Editor/Games/Index");


        }
    
    }

}
