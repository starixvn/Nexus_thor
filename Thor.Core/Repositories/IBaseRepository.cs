namespace Thor.Core.Repositories
{
    using System.Threading.Tasks;
    using Fanex.Data.Repository;
    using Thor.Core.Models;

    public interface IBaseRepository
    {
        Task<CommandResult> ExecuteNonQueryAsync(INonQueryCommand command);
    }
}
