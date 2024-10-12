using System.Data;

namespace Amplifund_API_Example.Endpoints
{
    public class TestEntity : BaseEndpoint
    {
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public int TestInt { get; set; }
        public string? TestString { get; set; }
        public double TestFloat { get; set; }
        public bool TestBit { get; set; }
    }
}
