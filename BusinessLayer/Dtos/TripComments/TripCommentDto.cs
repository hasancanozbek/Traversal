﻿namespace BusinessLayer.Dtos.Comments
{
    public class TripCommentDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int TripDateId { get; set; }
        public string Text { get; set; }
        public int Star { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerProfilePhotoUrl { get; set; }
        public string TripName { get; set; }
        public DateTime TripDate { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
    }
}
