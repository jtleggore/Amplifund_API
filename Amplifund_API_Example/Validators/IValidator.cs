using Amplifund_API_Example.Endpoints;
using Amplifund_API_Example.Repositories;

namespace Amplifund_API_Example.Validators
{
    /// <summary>
    /// Enables validation prior to or after repository call
    /// </summary>
    /// <typeparam name="T">Endpoint Type</typeparam>
    public interface IValidator<T> where T : BaseEndpoint
    {
        Task<IEnumerable<T>>? ValidateGetAll(Func<Request<T>?, Task<IEnumerable<T>>>? method, Request<T> request);
        Task<T>? ValidateGet(Func<Guid, Task<T>?> method, Guid id);
        Task<T>? ValidatePut(Func<T, Task<T>?> method, T entity);
        Task<T> ValidatePost(Func<T, Task<T>> method, T entity);
        Task<T>? ValidateDelete(Func<Guid, Task<T>> method, Guid id);
    }
}
