using Microsoft.EntityFrameworkCore;
using Shop.Core.Domain;
using Shop.Core.Domain.Spaceship;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Shop.Data;
using Microsoft.Extensions.Hosting;

namespace Shop.ApplicationServices.Services
{
	public class FilesServices : IFilesServices
	{
		private readonly ShopContext _context;
		private readonly IHostEnvironment _webHost;
		public FilesServices
			(
				ShopContext context,
				IHostEnvironment webHost
			)
		{
			_context = context;
			_webHost = webHost;
		}

		public void UploadFilesToDatabase(SpaceshipDto dto, Spaceship domain)
		{
			if (dto.Files != null && dto.Files.Count > 0)
			{
				foreach (var photo in dto.Files)
				{
					using (var target = new MemoryStream())
					{
						FileToDatabase files = new FileToDatabase()
						{
							Id = Guid.NewGuid(),
							ImageTitle = photo.FileName,
							SpaceshipId = domain.Id,
						};
						photo.CopyTo(target);
						files.ImageData = target.ToArray();

						_context.FileToDatabase.Add(files);
					}
				}

			}
			
		}

		public async Task<FileToDatabase> RemoveImage(FileToDatabaseDto dto)
		{
			var image = await _context.FileToDatabase
				.Where(x => x.Id == dto.Id)
				.FirstOrDefaultAsync();

			_context.FileToDatabase.Remove(image);
			await _context.SaveChangesAsync();

			return image;
		}

		public async Task<List<FileToDatabase>> RemoveImagesFromDatabase(FileToDatabaseDto[] dtos)
		{
			foreach (var dto in dtos)
			{
				var image = await _context.FileToDatabase
				.Where(x => x.Id == dto.Id)
				.FirstOrDefaultAsync();

				_context.FileToDatabase.Remove(image);
				await _context.SaveChangesAsync();
			}

			return null;
		}

		public void FilesToApi(RealEstateDto dto, RealEstate realEstate)
		{
			string uniqueFileName = null;

			if (dto.Files != null && dto.Files.Count > 0)
			{
				if (!Directory.Exists(_webHost.ContentRootPath + "\\multipleFileUpload\\"))
				{
					Directory.CreateDirectory(_webHost.ContentRootPath + "\\multipleFileUpload\\");
				}

				foreach (var image in dto.Files)
				{
					string uploadsFolder = Path.Combine(_webHost.ContentRootPath, "multipleFileUpload");
					uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
					string filePath  = Path.Combine(uploadsFolder, uniqueFileName);

					using (var fileStream = new FileStream(filePath, FileMode.Create))
					{
						image.CopyTo(fileStream);

						FileToApi path = new FileToApi
						{
							Id = Guid.NewGuid(),
							ExistingFilePath = uniqueFileName,
							RealEstateId = realEstate.Id,
						};

						_context.FileToApis.AddAsync(path);
					}
				}
			}

		}

		public async Task<List<FileToApi>> RemoveImagesFromApi(FileToApiDto[] dtos)
		{
			foreach (var dto in dtos)
			{
				var imageId = await _context.FileToApis
					.FirstOrDefaultAsync(x => x.ExistingFilePath == dto.ExistingFilePath);
				var filePath = _webHost.ContentRootPath + "\\mutipleFileUpload\\" + imageId.ExistingFilePath;

				if (File.Exists(filePath))
				{
					File.Delete(filePath);
				}

				_context.FileToApis.Remove(imageId);
				await _context.SaveChangesAsync();
			}

			return null;

		}

		public async Task<FileToApi> RemoveImageFromApi(FileToApiDto dto)
		{
			
				var imageId = await _context.FileToApis
					.FirstOrDefaultAsync(x => x.Id == dto.Id);

				var filePath = _webHost.ContentRootPath + "\\mutipleFileUpload\\" + imageId.ExistingFilePath;

				if (File.Exists(filePath))
				{
					File.Delete(filePath);
				}

				_context.FileToApis.Remove(imageId);
				await _context.SaveChangesAsync();
		

			return null;

		}
	}
}
