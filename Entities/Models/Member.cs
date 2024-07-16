using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    public class Member
    {
        [Key]
        public string Id { get; set; } = string.Empty;

        [ForeignKey(nameof(Id))]
        public ApplicationUser? ApplicationUser { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string? EducationDegree { get; set; } = string.Empty;

        public List<Loan>? Loans { get; set; }

        public List<Penalty>? Penalties { get; set; }
    }
}
