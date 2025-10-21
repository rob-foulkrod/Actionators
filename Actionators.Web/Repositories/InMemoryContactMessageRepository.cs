using Actionators.Web.Models;

namespace Actionators.Web.Repositories;

public class InMemoryContactMessageRepository : IContactMessageRepository
{
    private readonly List<ContactMessage> _messages = new();
    private int _nextId = 1;
    private readonly object _lock = new();

    public Task<IEnumerable<ContactMessage>> GetAllAsync()
    {
        lock (_lock)
        {
            return Task.FromResult<IEnumerable<ContactMessage>>(_messages.ToList());
        }
    }

    public Task<ContactMessage?> GetByIdAsync(int id)
    {
        lock (_lock)
        {
            var message = _messages.FirstOrDefault(m => m.Id == id);
            return Task.FromResult(message);
        }
    }

    public Task AddAsync(ContactMessage message)
    {
        lock (_lock)
        {
            message.Id = _nextId++;
            message.CreatedAt = DateTime.UtcNow;
            _messages.Add(message);
        }
        return Task.CompletedTask;
    }

    public Task<bool> DeleteAsync(int id)
    {
        lock (_lock)
        {
            var message = _messages.FirstOrDefault(m => m.Id == id);
            if (message != null)
            {
                _messages.Remove(message);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
