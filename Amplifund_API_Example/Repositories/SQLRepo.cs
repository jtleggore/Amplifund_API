using Amplifund_API_Example.Contexts;
using Amplifund_API_Example.Endpoints;

namespace Amplifund_API_Example.Repositories
{
    public class SQLRepo<T> : IRepo<T> where T : BaseEndpoint
    {
        private readonly SqlDataContext _context;

        public SQLRepo(SqlDataContext context)
        {
            _context = context;
        }

        public async Task<T> Add(T entity)
        {
            var addedEntity = (await _context.AddAsync(entity)).Entity;
            _context.SaveChanges();
            return addedEntity;
        }

        public async Task<T>? Delete(Guid entityId)
        {
            var entity = await _context.FindAsync<T>(entityId);
            if (entity != null) _context.Remove(entity);
            //else throw new InvalidOperationException("Entity does not exist!");
            _context.SaveChanges();
            return entity;
        }

        public async Task<IEnumerable<T>> GetAll(Request<T> request)
        {
            IQueryable<T> query = _context.Set<T>();

            if (request?.Filter != null)
            {
                query = query.Where(request.Filter);
            }

            if (request?.OrderBy != null)
            {
                query = request.OrderBy(query);
            }

            if (request?.Skip != null && request.Skip.HasValue)
            {
                query = query.Skip(request.Skip.Value);
            }

            if (request?.Take != null && request.Take.HasValue)
            {
                query = query.Take(request.Take.Value);
            }

            return [.. query];
        }

        public async Task<T>? GetById(Guid entityId)
        {
            return await _context.FindAsync<T>(entityId);
        }

        public async Task<T> Update(T entity)
        {
            var updatedEntity = _context.Update(entity).Entity;
            await _context.SaveChangesAsync();
            return updatedEntity;
        }
    }
}
