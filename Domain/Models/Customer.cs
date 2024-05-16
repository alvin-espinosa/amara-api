namespace Domain.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
