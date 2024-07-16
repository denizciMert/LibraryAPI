namespace LibraryAPI.Entities.DTOs.CountryDTO
{
    public class CountryGet
    {
        public int? Id { get; set; }
        public string? CountryName { get; set; } = string.Empty;
        public DateTime? CreatinDateLog { get; set; }
        public DateTime? UpdateDateLog { get; set; }
        public DateTime? DeleteDateLog { get; set; }
        public string? State { get; set; }
    }
}
