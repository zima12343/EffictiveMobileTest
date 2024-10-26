using DAL.Models.Areas;

namespace DataAccess.Repository.Abstraction
{
    public interface IAreaRepository
    {
        Task<AreaDto> Add(AddAreaDto dto);
        Task RemoveAll();
    }
}
