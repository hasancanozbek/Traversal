﻿namespace BusinessLayer.Dtos.Customer
{
    public class AddCustomerDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CellPhone { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
