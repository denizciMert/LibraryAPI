using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAPI.Entities.DTOs.ShiftDTO
{
    public class ShiftPost
    {
        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string ShiftType { get; set; }
    }
}
