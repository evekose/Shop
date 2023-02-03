using Shop.Core.Domain.Spaceship;
using Shop.Core.Domain;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Shop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Core.Domain.Car;
using Microsoft.EntityFrameworkCore;

namespace Shop.ApplicationServices.Services
{
	public class FilesServices : IFilesServices
	{
		private readonly ShopContext _context;

		public FilesServices
			(
				ShopContext context
			)
		{
			_context = context;
		}

		public void UploadFilesToDatabase(CarDto dto, Car domain)
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
							CarId = domain.Id,
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
	}
}
