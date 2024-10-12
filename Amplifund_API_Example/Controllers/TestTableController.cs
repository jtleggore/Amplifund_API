using Amplifund_API_Example.Endpoints;
using Amplifund_API_Example.Repositories;
using Amplifund_API_Example.Validators;
using Microsoft.AspNetCore.Mvc;

namespace Amplifund_API_Example.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestTableController : GenericController<TestEntity>
    {
        public TestTableController(IRepo<TestEntity> repository, IValidator<TestEntity> validator) : base(repository, validator)
        {
        }
    }
}
