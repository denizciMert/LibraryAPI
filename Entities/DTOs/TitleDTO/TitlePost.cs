using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAPI.Entities.DTOs.TitleDTO
{
    public class TitlePost
    {
        [Required]
        [StringLength(100)]
        public string TitleName { get; set; } = string.Empty;
    }
}
