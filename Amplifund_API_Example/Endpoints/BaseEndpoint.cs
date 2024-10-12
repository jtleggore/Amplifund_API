using Microsoft.Identity.Client;

namespace Amplifund_API_Example.Endpoints
{
    /// <summary>
    /// Fields that all endpoints have
    /// </summary>
    public abstract class BaseEndpoint
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
