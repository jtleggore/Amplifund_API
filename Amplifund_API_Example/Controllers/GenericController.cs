using Amplifund_API_Example.Endpoints;
using Amplifund_API_Example.Repositories;
using Amplifund_API_Example.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Amplifund_API_Example.Controllers
{
    /// <summary>
    /// Child controllers can implement this class with no code except a constructor for full endpoint functionality
    /// </summary>
    /// <typeparam name="T">Endpoint Type</typeparam>
    [ApiController]
    [Route("[controller]")]
    public class GenericController<T> : ControllerBase, IApiController<T> where T : BaseEndpoint
    {
        private readonly IRepo<T> _repo;
        private readonly IValidator<T> _validator;

        public GenericController(IRepo<T> r, IValidator<T> v)
        {
            _repo = r;
            _validator = v;
        }

        [HttpPost("GetAll")]
        public async Task<IEnumerable<T>>? GetAll([FromBody] Request<T>? request = null)
        {
            return await _validator.ValidateGetAll(_repo.GetAll, request);
        }

        [HttpGet("{id}")]
        public async Task<T>? Get(Guid id)
        {
            return await _validator.ValidateGet(_repo.GetById, id);
        }

        [HttpPost("Add")]
        public async Task<T> Post(T entity)
        {
            return await _validator.ValidatePost(_repo.Add, entity);
        }

        [HttpPut("Update")]
        public async Task<T>? Put(T entity)
        {
            return await _validator.ValidatePut(_repo.Update, entity);
        }

        [HttpDelete("{id}")]
        public async Task<T>? Delete(Guid id)
        {
            return await _validator.ValidateDelete(_repo.Delete, id);
        }
    }
}
