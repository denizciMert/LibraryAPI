namespace LibraryAPI.Entities.DTOs.LocationDTO
{
    public class LocationGet
    {
        // ID of the location
        public int? Id { get; set; }

        // Code representing the shelf
        public string ShelfCode { get; set; } = string.Empty;

        // Log date when the record was created
        public DateTime? CreatinDateLog { get; set; }

        // Log date when the record was last updated
        public DateTime? UpdateDateLog { get; set; }

        // Log date when the record was deleted
        public DateTime? DeleteDateLog { get; set; }

        // State of the location
        public string? State { get; set; }
    }
}