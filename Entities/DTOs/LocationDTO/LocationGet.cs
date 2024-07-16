namespace LibraryAPI.Entities.DTOs.LocationDTO
{
    public class LocationGet
    {
        public int? Id { get; set; }
        public string ShelfCode { get; set; } = string.Empty;
        public DateTime? CreatinDateLog { get; set; }
        public DateTime? UpdateDateLog { get; set; }
        public DateTime? DeleteDateLog { get; set; }
        public string? State { get; set; }
    }
}
