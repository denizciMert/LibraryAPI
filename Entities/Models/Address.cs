using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    public class Address : AbstractEntity
    {
        [Column(TypeName = "varchar(500)")]
        public string AddressString { get; set; } = string.Empty;

        public int DistrictId { get; set; }

        [ForeignKey(nameof(DistrictId))]
        public District? District { get; set; }

        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser? ApplicationUser { get; set; }
    }
}
