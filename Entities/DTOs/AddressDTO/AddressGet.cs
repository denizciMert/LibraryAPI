namespace LibraryAPI.Entities.DTOs.AddressDTO
{
    public class AddressGet
    {
        public int? Id { get; set; }
        public string? AddressString { get; set; } = string.Empty;
        public string? District { get; set; } = string.Empty;
        public string? City { get; set; } = string.Empty;
        public string? Country { get; set; } = string.Empty;
        public string? UserName { get; set; } = string.Empty;
        public string? UserFullName { get; set; } = string.Empty;
        public DateTime? CreatinDateLog { get; set; }
        public DateTime? UpdateDateLog { get; set; }
        public DateTime? DeleteDateLog { get; set; }
        public string? State { get; set; }
    }
}
