using Amplifund_API_Example.Endpoints;
using Amplifund_API_Example.Repositories;
using Amplifund_API_Example.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Windows.Input;

namespace Amplifund_API_Example.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : GenericController<Person>
    {
        public PersonController(IRepo<Person> repository, IValidator<Person> validator) : base(repository, validator)
        {
        }
    }
}
