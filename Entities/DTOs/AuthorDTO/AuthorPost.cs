using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAPI.Entities.DTOs.AuthorDTO
{
    public class AuthorPost
    {
        [Required]
        [StringLength(100)]
        public string AuthorName { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Biography { get; set; } = string.Empty;

        [Required]
        [Range(-4000, 2100)]
        public short DateOfBirth { get; set; } = 0;

        [Range(-4000, 2100)]
        public short? DateOfDeath { get; set; }
    }
}
