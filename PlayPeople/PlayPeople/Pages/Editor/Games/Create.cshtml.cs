using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlayPeople.Models;
using PlayPeople.Services;
using System.Drawing;
using System.Net.Http.Headers;

namespace PlayPeople.Pages.Editor.Games
{
	public class CreateModel : PageModel
	{
		private readonly IWebHostEnvironment environment;
		private readonly ApplicationDbContext context;

		[BindProperty]
		public GameDto GameDto { get; set; } = new GameDto();

		public CreateModel(IWebHostEnvironment environment, ApplicationDbContext context)
		{
			this.environment = environment;
			this.context = context;
		}
		public void OnGet()
		{
		}

		public string errorMessage = "";
		public string successMessage = "";

		public void OnPost()
		{
			if (GameDto.ImageFile == null)
			{
				ModelState.AddModelError("GameDto.ImageFile", "Необходимо добавить изображение");
			}

			if (!ModelState.IsValid)
			{
				errorMessage = "Пожалуйста, заполните все необходимые поля";
				return;
			}

			string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
			newFileName += Path.GetExtension(GameDto.ImageFile!.FileName);

			string imageFullPath = environment.WebRootPath + "/games/" + newFileName;
			using (var stream = System.IO.File.Create(imageFullPath))
			{
				GameDto.ImageFile.CopyTo(stream);
			}

			Game game = new Game()
			{
				Title = GameDto.Title,
				Developer = GameDto.Developer,
				Genre = GameDto.Genre,	
				Description = GameDto.Description ?? "",
				ReleaseDate = GameDto.ReleaseDate,
				Platforms = GameDto.Platforms,
				ImageFileName = newFileName,
			};

			context.Games.Add(game);
			context.SaveChanges();

			GameDto.Title = "";
			GameDto.Developer = "";
			GameDto.Genre = "";
			GameDto.Description = "";
			GameDto.ReleaseDate = DateOnly.MinValue;
			GameDto.Platforms = "";
			GameDto.ImageFile = null;

			ModelState.Clear();

			successMessage = "Игра добавлена успешно";

			Response.Redirect("/Editor/Games/Index");
		}
	}
}
