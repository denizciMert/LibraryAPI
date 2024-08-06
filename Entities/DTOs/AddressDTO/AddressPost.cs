using System.Text.Json.Serialization;

namespace LibraryAPI.Entities.DTOs.AddressDTO
{
    public class AddressPost
    {
        // The address string (e.g., street address)
        public string AddressString { get; set; } = string.Empty;

        // Unique identifier for the district associated with this address
        public int DistrictId { get; set; }

        // User ID of the user associated with this address
        // This property is ignored during JSON serialization
        [JsonIgnore]
        public string UserId { get; set; } = string.Empty;
    }
}