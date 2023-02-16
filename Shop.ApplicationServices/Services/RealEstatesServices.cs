using Microsoft.EntityFrameworkCore;
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
	public class RealEstatesServices : IRealEstatesServices
	{
		private readonly ShopContext _context;
		private readonly IFilesServices _filesServices;
		
		public RealEstatesServices
			(
				ShopContext context,
				IFilesServices filesServices
			)
		{
			_context = context;
			_filesServices = filesServices;
		}


		public async Task<RealEstate> Create(RealEstateDto dto)
		{
			RealEstate realEstate = new();

			realEstate.Id = Guid.NewGuid();
			realEstate.Address = dto.Address;
			realEstate.City = dto.City;
			realEstate.Region = dto.Region;
			realEstate.PostalCode = dto.PostalCode;
			realEstate.Country = dto.Country;
			realEstate.Phone = dto.Phone;
			realEstate.Fax = dto.Fax;
			realEstate.Size = dto.Size;
			realEstate.Floor = dto.Floor;
			realEstate.Price = dto.Price;
			realEstate.RoomCount = dto.RoomCount;
			realEstate.ModifiedAt = DateTime.Now;
			realEstate.CreatedAt = DateTime.Now;
			_filesServices.FilesToApi(dto, realEstate);

			await _context.RealEstates.AddAsync(realEstate);
			await _context.SaveChangesAsync();

			return realEstate;

		}
		public async Task<RealEstate> Update(RealEstateDto dto)
		{
			RealEstate realEstate = new();

			realEstate.Id = dto.Id;
			realEstate.Address = dto.Address;
			realEstate.City = dto.City;
			realEstate.Region = dto.Region;
			realEstate.PostalCode = dto.PostalCode;
			realEstate.Country = dto.Country;
			realEstate.Phone = dto.Phone;
			realEstate.Fax = dto.Fax;
			realEstate.Size = dto.Size;
			realEstate.Floor = dto.Floor;
			realEstate.Price = dto.Price;
			realEstate.RoomCount = dto.RoomCount;
			realEstate.CreatedAt = dto.CreatedAt;
			realEstate.ModifiedAt = DateTime.Now;
			_filesServices.FilesToApi(dto, realEstate);

			_context.RealEstates.Update(realEstate);
			await _context.SaveChangesAsync();

			return realEstate;

		}

		public async Task<RealEstate> Delete(Guid id)
		{
			var realEstateId = await _context.RealEstates
				.Include(x => x.FileToApis)
				.FirstOrDefaultAsync(x => x.Id == id);

			var images = await _context.FileToApis
				.Where(x => x.RealEstateId == id)
				.Select(y => new FileToApiDto
				{
					Id = y.Id,
					RealEstateId = y.RealEstateId,
					ExistingFilePath = y.ExistingFilePath
				}).ToArrayAsync();

			await _filesServices.RemoveImagesFromApi(images);
			_context.RealEstates.Remove(realEstateId);
			await _context.SaveChangesAsync();

			return realEstateId;
		}

		public async Task<RealEstate> GetAsync(Guid id)
		{
			var result = await _context.RealEstates
				.FirstOrDefaultAsync(x => x.Id == id);

			return result;
		}
	}
}
