namespace BusinessLayer.Dtos.Customers
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string ProfilePhoto { get; set; }
        public bool IsActive { get; set; }
    }
}
