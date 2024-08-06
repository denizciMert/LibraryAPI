namespace LibraryAPI.Entities.DTOs.DistrictDTO
{
    public class DistrictGet
    {
        // Unique identifier for the district
        public int? Id { get; set; }

        // Name of the district
        public string District { get; set; } = string.Empty;

        // Name of the city to which the district belongs
        public string City { get; set; } = string.Empty;

        // Name of the country to which the city belongs
        public string Country { get; set; } = string.Empty;

        // Timestamp when the record was created
        public DateTime? CreatinDateLog { get; set; }

        // Timestamp when the record was last updated
        public DateTime? UpdateDateLog { get; set; }

        // Timestamp when the record was deleted
        public DateTime? DeleteDateLog { get; set; }

        // State of the district (active, inactive, etc.)
        public string? State { get; set; }
    }
}