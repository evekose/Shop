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
	public interface IRealEstatesServices
	{
		Task<RealEstate> GetAsync(Guid id);
		Task<RealEstate> Create(RealEstateDto dto);
		Task<RealEstate> Delete(Guid id);
		Task<RealEstate> Update(RealEstateDto dto);


	}
}
