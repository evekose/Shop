using Microsoft.EntityFrameworkCore;
using Shop.Core.Domain;
using Shop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.ApplicationServices.Services
{
	public class RealEstatesServices
	{
		private readonly ShopContext _context;
		
		public RealEstatesServices
			(
				ShopContext context
			)
		{
			_context = context;
		}

	public async Task<RealEstate> GetRealAsync(Guid id)
	{
		var result = await _context.RealEstates
				.FirstOrDefaultAsync(x=> x.Id == id);

			return result;
	}

	}
}
