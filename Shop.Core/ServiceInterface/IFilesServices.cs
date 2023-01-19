using Shop.Core.Domain;
using Shop.Core.Domain.Spaceship;
using Shop.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.ServiceInterface
{
	public interface IFilesServices
	{
		void UploadFilesToDatabase(SpaceshipDto dto, Spaceship domain);
		Task<FileToDatabase> RemoveImage(FileToDatabaseDto dto);
	}
}
