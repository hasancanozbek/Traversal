﻿using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Dtos.Customers
{
    public class UpdateCustomerDto
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? CellPhone { get; set; }
        public IFormFile? ProfilePhoto { get; set; }
    }
}
