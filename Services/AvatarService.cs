using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;

namespace CardGamesApp.Services;

public class AvatarService
{
	public async Task<string?> PickAvatarAsync(Window owner)
	{
		var files = await owner.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
		{
			Title = "Wybierz avatar",
			AllowMultiple = false,
			FileTypeFilter =
			[
				new FilePickerFileType("Obrazy")
				{
					Patterns = ["*.png", "*.jpg", "*.jpeg", "*.webp"]
				}
			]
		});

		if (files.Count == 0)
			return null;

		return files[0].Path.LocalPath;
	}
}