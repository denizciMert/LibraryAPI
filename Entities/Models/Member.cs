using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    // Member model class.
    // Represents a library member, with a unique identifier and related information.
    public class Member
    {
        // Primary key for the Member entity.
        // Column type is nvarchar(450) to accommodate the unique identifier.
        [Key]
        [Column(TypeName = "nvarchar(450)")]
        public string Id { get; set; } = string.Empty;

        // Navigation property to the ApplicationUser entity.
        // Establishes a one-to-one relationship with the IdentityUser.
        [ForeignKey(nameof(Id))]
        public ApplicationUser? ApplicationUser { get; set; }

        // Represents the member's education degree.
        // Column type is varchar(50) to store degrees up to 50 characters long.
        [Column(TypeName = "varchar(50)")]
        public string? EducationDegree { get; set; } = string.Empty;

        // Collection of loans associated with the member.
        // Indicates all the books the member has borrowed.
        public List<Loan>? Loans { get; set; }

        // Collection of penalties associated with the member.
        // Represents any fines or penalties the member has accrued.
        public List<Penalty>? Penalties { get; set; }
    }
}