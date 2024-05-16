using Data;
using Domain.Models;

namespace Infrastructure.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>
    {
        public CustomerRepository(RentingContext context) : base(context) { }

        public override Customer Update(Customer entity)
        {
            // var customer = context.Customers.Single(c => c.Id == entity.Id)

            // cusomer.ContactNumber = entity.ContactNumber
            // and so on....

            return base.Update(entity);
        }
    }
}
