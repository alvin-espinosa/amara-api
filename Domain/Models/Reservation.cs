namespace Domain.Models
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public required string PickupPlace { get; set; }
        public DateTime PickupDateTime { get; set; }
        public Customer Customer { get; set; }
        public Guid CustomerId { get; set; }
    }
}
