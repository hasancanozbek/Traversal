﻿using EntityLayer.Abstracts;
namespace EntityLayer.Concretes
{
    public class Customer : IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime? BirthDate { get; set; }

        //Navigation Properties
        public virtual List<Blog> BlogList { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}
