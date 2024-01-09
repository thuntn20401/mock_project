using InsternShip.Data.Entities;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.Itrsinterview;

namespace InsternShip.Data.Interfaces;

public interface IItrsinterviewRepository : IRepository<Itrsinterview>
{
    Task<IEnumerable<ItrsinterviewModel>> GetAllItrsinterview();
    Task<ItrsinterviewModel?> GetItrsinterviewById(Guid id);
    Task<ItrsinterviewModel?> SaveItrsinterview(ItrsinterviewModel request, Guid interviewerId);
    Task<bool> UpdateItrsinterview(ItrsinterviewModel request, Guid requestId);
    Task<bool> DeleteItrsinterview(Guid requestId);
}