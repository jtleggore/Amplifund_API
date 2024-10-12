using Amplifund_API_Example.Endpoints;
using Amplifund_API_Example.Repositories;

namespace Amplifund_API_Example.Validators
{
    public class GenericValidator<T> : IValidator<T> where T : BaseEndpoint
    {
        public async Task<T> ValidatePost(Func<T, Task<T>> method, T entity)
        {
            var result = await method(entity);
            if (result is null)
            {
                throw new InvalidOperationException("Entity is invalid!");
            }
            return result;
        }

        public async Task<T>? ValidateDelete(Func<Guid, Task<T>> method, Guid id)
        {
            var result = await method(id);
            if (result is null)
            {
                throw new InvalidOperationException("Entity does not exist!");
            }
            return result;
        }

        public async Task<T>? ValidateGet(Func<Guid, Task<T>?> method, Guid id)
        {
            var result = await method(id);
            if (result is null)
            {
                throw new InvalidOperationException("Entity does not exist!");
            }
            return result;
        }

        public async Task<IEnumerable<T>>? ValidateGetAll(Func<Request<T>?, Task<IEnumerable<T>>>? method, Request<T> request)
        {
            //todo: consideration for the future, validate request params

            var result = await method(request);
            if (result is null)
            {
                throw new InvalidOperationException("Entity results is empty!");
            }
            return result;
        }

        public async Task<T>? ValidatePut(Func<T, Task<T>?> method, T entity)
        {
            T result;
            try
            {
                result = await method(entity);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new InvalidOperationException("Entity does not exist!", ex);
            }

            if (result is null)
            {
                throw new InvalidOperationException("Entity does not exist!");
            }
            return result;
        }
    }
}
