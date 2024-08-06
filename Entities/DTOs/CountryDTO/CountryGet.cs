namespace LibraryAPI.Entities.DTOs.CountryDTO
{
    public class CountryGet
    {
        // The unique identifier for the country, nullable to handle cases where the ID may not be available.
        public int? Id { get; set; }

        // The name of the country.
        public string? CountryName { get; set; } = string.Empty;

        // Logs for tracking creation, update, and deletion dates.
        public DateTime? CreatinDateLog { get; set; }
        public DateTime? UpdateDateLog { get; set; }
        public DateTime? DeleteDateLog { get; set; }

        // Represents the state of the country record (e.g., active, inactive).
        public string? State { get; set; }
    }
}