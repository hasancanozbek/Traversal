﻿namespace BusinessLayer.Dtos.Locations
{
    public class LocationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public string Detail { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
    }
}
