namespace LibraryAPI.Entities.DTOs.AddressDTO
{
    public class AddressPost
    {
        public string AddressString { get; set; } = string.Empty;
        public int DistrictId { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}
