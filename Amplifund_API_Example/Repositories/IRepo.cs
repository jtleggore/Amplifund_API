using Amplifund_API_Example.Endpoints;
using System.Linq.Expressions;

namespace Amplifund_API_Example.Repositories
{
    /// <summary>
    /// Can be implemented to dynamically change DB type to connect to
    /// </summary>
    /// <typeparam name="T">Endpoint Type</typeparam>
    public interface IRepo<T> where T : BaseEndpoint
    {
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T>? Delete(Guid entityId);
        Task<IEnumerable<T>> GetAll(Request<T>? request);
        Task<T>? GetById(Guid entityId);
    }

    /// <summary>
    /// Handles filtering, ordering, skipping, and taking on GetAll POST operation
    /// </summary>
    public class Request<T> where T : BaseEndpoint
    {
        public Expression<Func<T, bool>>? Filter { get; set; }
        public Func<IQueryable<T>, IOrderedQueryable<T>>? OrderBy { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}
