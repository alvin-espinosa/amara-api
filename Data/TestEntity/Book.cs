namespace Data.TestEntity
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Author Author { get; set; } = new();
        public int AuthorId { get; set; }

    }
}
