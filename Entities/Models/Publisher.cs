using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    // Publisher model class, inheriting from AbstractEntity.
    // Represents a publisher of books.
    public class Publisher : AbstractEntity
    {
        // Name of the publisher (e.g., "Penguin Random House").
        // Column type is varchar with a maximum length of 450 characters.
        [Column(TypeName = "varchar(450)")]
        public string PublisherName { get; set; } = string.Empty;

        // Phone number of the publisher's contact.
        // Column type is varchar with a maximum length of 15 characters.
        [Column(TypeName = "varchar(15)")]
        public string? Phone { get; set; }

        // Email address of the publisher.
        // Column type is varchar with a maximum length of 320 characters.
        [Column(TypeName = "varchar(320)")]
        public string? EMail { get; set; }

        // Name of the contact person at the publisher.
        // Column type is varchar with a maximum length of 450 characters.
        [Column(TypeName = "varchar(450)")]
        public string? ContactPerson { get; set; }
    }
}