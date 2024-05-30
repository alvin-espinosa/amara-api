namespace Data.TestEntity
{
    public abstract class Person
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateOnly Birthdate { get; set; }
    }
}
