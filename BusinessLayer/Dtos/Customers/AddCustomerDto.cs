namespace BusinessLayer.Dtos.Customers
{
    public class AddCustomerDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CellPhone { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
