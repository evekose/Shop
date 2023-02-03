using Shop.Core.Domain.Car;
using Shop.Core.Domain.Spaceship;
using Shop.Core.Dto;


namespace Shop.Core.ServiceInterface
{
    public interface ICarsServices
    {
		Task<Car> Create(CarDto dto);

		Task<Car> Update(CarDto dto);

		Task<Car> Delete(Guid id);

		Task<Car> GetAsync(Guid id);
	}
}
