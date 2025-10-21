using Actionators.Web.Models;

namespace Actionators.Web.Repositories;

public interface IContactMessageRepository
{
    Task<IEnumerable<ContactMessage>> GetAllAsync();
    Task<ContactMessage?> GetByIdAsync(int id);
    Task AddAsync(ContactMessage message);
    Task<bool> DeleteAsync(int id);
}
