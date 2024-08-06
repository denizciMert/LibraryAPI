namespace LibraryAPI.Entities.DTOs.AddressDTO
{
    public class AddressGet
    {
        // Unique identifier for the address
        public int? Id { get; set; }

        // The address string (e.g., street address)
        public string? AddressString { get; set; } = string.Empty;

        // Name of the district where the address is located
        public string? District { get; set; } = string.Empty;

        // Name of the city where the address is located
        public string? City { get; set; } = string.Empty;

        // Name of the country where the address is located
        public string? Country { get; set; } = string.Empty;

        // Username of the user associated with this address
        public string? UserName { get; set; } = string.Empty;

        // Full name of the user associated with this address
        public string? UserFullName { get; set; } = string.Empty;

        // Date when the address was created
        public DateTime? CreatinDateLog { get; set; }

        // Date when the address was last updated
        public DateTime? UpdateDateLog { get; set; }

        // Date when the address was deleted
        public DateTime? DeleteDateLog { get; set; }

        // State or status of the address (e.g., active, inactive)
        public string? State { get; set; }
    }
}