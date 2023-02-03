using Shop.Core.Domain.Spaceship;
using Shop.Core.Domain;
using Shop.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Core.Domain.Car;

namespace Shop.Core.ServiceInterface
{
	public interface IFilesServices
	{
		void UploadFilesToDatabase(CarDto dto, Car domain);
		Task<FileToDatabase> RemoveImage(FileToDatabaseDto dto);
		Task<List<FileToDatabase>> RemoveImagesFromDatabase(FileToDatabaseDto[] dtos);
	}
}
