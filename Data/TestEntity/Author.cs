using Microsoft.EntityFrameworkCore;

namespace Data.TestEntity
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Book> Books { get; set; } = new();

        public static void ConfigureDB(ModelBuilder mb)
        {
            mb.Entity<Author>(entity =>
            {
                entity
                    .HasMany(a => a.Books)
                    .WithOne(b => b.Author)
                    .HasForeignKey(b => b.AuthorId);                
            });

            //mb.Entity<Book>().upda
        }
    }
}
