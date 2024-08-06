namespace LibraryAPI.Entities.DTOs.CityDTO
{
    public class CityGet
    {
        // The unique identifier for the city.
        public int? Id { get; set; }

        // The name of the city.
        public string? CityName { get; set; } = string.Empty;

        // The name of the country where the city is located.
        public string? CountryName { get; set; } = string.Empty;

        // The date when the city record was created.
        public DateTime? CreatinDateLog { get; set; }

        // The date when the city record was last updated.
        public DateTime? UpdateDateLog { get; set; }

        // The date when the city record was deleted (if applicable).
        public DateTime? DeleteDateLog { get; set; }

        // The state or status of the city record.
        public string? State { get; set; }
    }
}