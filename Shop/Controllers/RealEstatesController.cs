﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.ApplicationServices.Services;
using Shop.Core.Domain;
using Shop.Core.Domain.Spaceship;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Shop.Data;
using Shop.Models.RealEstate;
using Shop.Models.Spaceship;

namespace Shop.Controllers
{
	public class RealEstatesController : Controller
	{
		private readonly IRealEstatesServices _realEstatesServices;
		private readonly ShopContext _context;
		private readonly IFilesServices _filesServices;
		public RealEstatesController
			(
				IRealEstatesServices realEstatesServices,
				ShopContext context,
				IFilesServices filesServices

			)

		{
			_realEstatesServices = realEstatesServices;
			_context = context;
			_filesServices = filesServices;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var result = _context.RealEstates
				.OrderByDescending(y => y.CreatedAt)
				.Select(x => new RealEstateIndexViewModel
				{
					Id = x.Id,
					Address = x.Address,
					City = x.City,
					Country = x.Country,
					Size = x.Size,
					Price = x.Price,
				});

			return View(result);

		}
		[HttpGet]
		public IActionResult Create()
		{
			RealEstateCreateUpdateViewModel vm = new RealEstateCreateUpdateViewModel();

			return View("CreateUpdate", vm);
		}


		[HttpPost]
		public async Task<IActionResult> Create(RealEstateCreateUpdateViewModel vm)
		{
			var dto = new RealEstateDto()
			{
				Id = vm.Id,
				Address = vm.Address,
				City = vm.City,
				Country = vm.Country,
				Size = vm.Size,
				Price = vm.Price,
				Floor = vm.Floor,
				Region = vm.Region,
				Phone = vm.Phone,
				Fax = vm.Fax,
				PostalCode = vm.PostalCode,
				RoomCount = vm.RoomCount,
				CreatedAt = vm.CreatedAt,
				ModifiedAt = vm.ModifiedAt,
				Files = vm.Files,
				FileToApiDtos = vm.FileToApiViewModels.Select(x => new FileToApiDto 
				
				{
					Id = x.ImageId,
					ExistingFilePath = x.FilePath,
					RealEstateId = x.RealEstateId
				}).ToArray()

			};

			var result = await _realEstatesServices.Create(dto);

			if (result == null)
			{
				return RedirectToAction(nameof(Index));
			}

			return RedirectToAction("Index", vm);
		}

		[HttpGet]
		public async Task<IActionResult> Update(Guid id)
		{
			var realEstate = await _realEstatesServices.GetAsync(id);

			if (realEstate == null)
			{
				return NotFound();
			}

			var images = await _context.FileToApis
				.Where(x => x.RealEstateId == id)
				.Select(y => new FileToApiViewModel
				{
					FilePath = y.ExistingFilePath, 
					ImageId = y.Id
				}).ToArrayAsync();

			var vm = new RealEstateCreateUpdateViewModel();

			vm.Id = realEstate.Id;
			vm.Address = realEstate.Address;
			vm.City = realEstate.City;
			vm.Region = realEstate.Region;
			vm.PostalCode = realEstate.PostalCode;
			vm.Country = realEstate.Country;
			vm.Phone = realEstate.Phone;
			vm.Fax = realEstate.Fax;
			vm.Size = realEstate.Size;
			vm.Floor = realEstate.Floor;
			vm.Price = realEstate.Price;
			vm.RoomCount = realEstate.RoomCount;
			vm.CreatedAt = realEstate.CreatedAt;
			vm.ModifiedAt = realEstate.ModifiedAt;
			vm.FileToApiViewModels.AddRange(images);

			return View("CreateUpdate", vm);
		}

		private void ToArrayAsync()
		{
			throw new NotImplementedException();
		}

		[HttpPost]
		public async Task<IActionResult> Update(RealEstateCreateUpdateViewModel vm)
		{
			var dto = new RealEstateDto()
			{
				Id = vm.Id,
				Address = vm.Address,
				City = vm.City,
				Region = vm.Region,
				PostalCode = vm.PostalCode,
				Country = vm.Country,
				Phone = vm.Phone,
				Fax = vm.Fax,
				Size = vm.Size,
				Floor = vm.Floor,
				Price = vm.Price,
				RoomCount = vm.RoomCount,
				CreatedAt = vm.CreatedAt,
				ModifiedAt = vm.ModifiedAt,
				Files = vm.Files,
				FileToApiDtos = vm.FileToApiViewModels
					.Select(x => new FileToApiDto
					{
						Id = x.ImageId,
						ExistingFilePath = x.FilePath,
						RealEstateId = x.RealEstateId
					}).ToArray()
			};

			var result = await _realEstatesServices.Update(dto);

			if (result == null)
			{
				return RedirectToAction(nameof(Index));
			}

			return RedirectToAction(nameof(Index), vm);
		}

		[HttpGet]
		public async Task<IActionResult> Details(Guid id)
		{
			var realEstate = await _realEstatesServices.GetAsync(id);

			if (realEstate == null)
			{
				return NotFound();
			}

			var images = await _context.FileToApis
				.Where(x => x.RealEstateId == id)
				.Select(y => new FileToApiViewModel
				{
					FilePath = y.ExistingFilePath,
					ImageId = y.Id
				}).ToArrayAsync();


			var vm = new RealEstateDetailsViewModel();

			vm.Id = realEstate.Id;
			vm.Address = realEstate.Address;
			vm.City = realEstate.City;
			vm.Region = realEstate.Region;
			vm.PostalCode = realEstate.PostalCode;
			vm.Country = realEstate.Country;
			vm.Phone = realEstate.Phone;
			vm.Fax = realEstate.Fax;
			vm.Size = realEstate.Size;
			vm.Floor = realEstate.Floor;
			vm.Price = realEstate.Price;
			vm.RoomCount = realEstate.RoomCount;
			vm.CreatedAt = realEstate.CreatedAt;
			vm.ModifiedAt = realEstate.ModifiedAt;
			vm.FileToApiViewModels.AddRange(images);


			return View(vm);
		}

		[HttpGet]
		public async Task<IActionResult> Delete(Guid id)
		{
			var realEstate = await _realEstatesServices.GetAsync(id);
			if (realEstate == null)
			{
				return NotFound();
			}

			var images = await _context.FileToApis
				.Where(x => x.RealEstateId == id)
				.Select(y => new FileToApiViewModel
				{
					FilePath = y.ExistingFilePath,
					ImageId = y.Id
				}).ToArrayAsync();


			var vm = new RealEstateDeleteViewModel();

			vm.Id = realEstate.Id;
			vm.Address = realEstate.Address;
			vm.City = realEstate.City;
			vm.Region = realEstate.Region;
			vm.PostalCode = realEstate.PostalCode;
			vm.Country = realEstate.Country;
			vm.Phone = realEstate.Phone;
			vm.Fax = realEstate.Fax;
			vm.Size = realEstate.Size;
			vm.Floor = realEstate.Floor;
			vm.Price = realEstate.Price;
			vm.RoomCount = realEstate.RoomCount;
			vm.CreatedAt = realEstate.CreatedAt;
			vm.ModifiedAt = realEstate.ModifiedAt;
			vm.FileToApiViewModels.AddRange(images);
			
			return View(vm);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteConfirmation(Guid id)
		{
			var realEstateId = await _realEstatesServices.Delete(id);
			if (realEstateId == null)
			{
				return RedirectToAction(nameof(Index));
			}

			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		public async Task<IActionResult> RemoveImage(FileToApiViewModel vm)
		{
			var dto = new FileToApiDto()
			{
				Id = vm.ImageId
			};

			var image = await _filesServices.RemoveImageFromApi(dto);

			if (image == null)
			{
				return RedirectToAction(nameof(Index));
			}

			return RedirectToAction(nameof(Index));
		}
		
	}
}
