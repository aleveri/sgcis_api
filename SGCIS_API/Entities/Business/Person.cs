namespace SGCIS_API.Entities.Business
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public PersonType PersonType { get; set; }
    }
}
