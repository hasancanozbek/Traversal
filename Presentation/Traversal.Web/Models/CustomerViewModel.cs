using BusinessLayer.Dtos.Customers;
using BusinessLayer.Dtos.CustomerTrips;

namespace Traversal.Web.Models
{
    public class CustomerViewModel
    {
        public AddCustomerDto AddCusmtomerModel { get; set; }
        public CustomerDto CustomerModel { get; set; }
        public List<CustomerTripDto> CustomerTripList { get; set; }
    }
}
