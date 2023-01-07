using Shop.Core.Domain.Car;
using Shop.Core.Dto;


namespace Shop.Core.ServiceInterface
{
    public interface ICarsServices
    {
        Task<Car> Add(CarDto dto);

        Task<Car> GetUpdate(Guid id);

        Task<Car> Update(CarDto dto);

        Task<Car> Delete(Guid id);

        Task<Car> GetAsync(Guid id);
    }
}
