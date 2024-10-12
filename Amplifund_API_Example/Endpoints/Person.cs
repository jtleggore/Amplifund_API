namespace Amplifund_API_Example.Endpoints
{
    public class Person : BaseEndpoint
    {
        public Person() { }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int Age {  get; set; }
        public string? Occupation { get; set; }
    }
}
