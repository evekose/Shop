using Shop.Core.Domain;
using Shop.Core.Domain.Spaceship;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Shop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
	}
}
