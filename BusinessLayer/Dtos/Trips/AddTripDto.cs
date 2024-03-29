﻿using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Dtos.Trips
{
    public class AddTripDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int Price { get; set; }
        public int GuideId { get; set; }
        public int Day { get; set; }
        public IFormFileCollection ImageList { get; set; }
    }
}
