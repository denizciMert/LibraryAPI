namespace LibraryAPI.Entities.DTOs.CityDTO
{
    public class CityGet
    {
        public int? Id { get; set; }
        public string? CityName { get; set; } = string.Empty;
        public string? CountryName { get; set; } = string.Empty;
        public DateTime? CreatinDateLog { get; set; }
        public DateTime? UpdateDateLog { get; set; }
        public DateTime? DeleteDateLog { get; set; }
        public string? State { get; set; }
    }
}
