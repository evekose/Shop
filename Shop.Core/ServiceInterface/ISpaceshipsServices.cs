using Shop.Core.Domain.Spaceship;
using Shop.Core.Dto;


namespace Shop.Core.ServiceInterface
{
    public interface ISpaceshipsServices
    {
        Task<Spaceship> Add(SpaceshipDto dto);

        Task<Spaceship> GetUpdate(Guid id);

        Task<Spaceship> Update(SpaceshipDto dto);

        Task<Spaceship> Delete(Guid id);
    }
}
