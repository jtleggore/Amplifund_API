using Amplifund_API_Example.Endpoints;
using Amplifund_API_Example.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Amplifund_API_Example.Controllers
{
    public interface IApiController<T> where T : BaseEndpoint
    {
        public Task<IEnumerable<T>>? GetAll(Request<T>? request);
        public Task<T>? Get(Guid id);
        public Task<T> Post(T entity);
        public Task<T>? Put(T entity);
        public Task<T>? Delete(Guid id);
    }
}
