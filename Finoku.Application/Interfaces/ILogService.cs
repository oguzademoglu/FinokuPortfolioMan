using Finoku.Domain.Entities;

namespace Finoku.Application.Interfaces
{
    public interface ILogService
    {
        Task LogAsync(SystemLog log);
    }
}
