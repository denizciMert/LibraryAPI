using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    // Address model class that inherits from AbstractEntity
    public class Address : AbstractEntity
    {
        // The address string, stored as a variable-length character string with a maximum length of 500 characters.
        [Column(TypeName = "varchar(500)")]
        public string AddressString { get; set; } = string.Empty;

        // The ID of the district where the address is located.
        public int DistrictId { get; set; }

        // Navigation property for the related District entity.
        [ForeignKey(nameof(DistrictId))]
        public District? District { get; set; }

        // The ID of the user associated with this address, stored as a variable-length Unicode string with a maximum length of 450 characters.
        [Column(TypeName = "nvarchar(450)")]
        public string UserId { get; set; } = string.Empty;

        // Navigation property for the related ApplicationUser entity.
        [ForeignKey(nameof(UserId))]
        public ApplicationUser? ApplicationUser { get; set; }
    }
}