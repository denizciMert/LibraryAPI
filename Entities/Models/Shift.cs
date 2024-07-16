using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAPI.Entities.Models
{
    public class Shift : AbstractEntity
    {
        [Column(TypeName = "varchar(20)")]
        public string ShiftType { get; set; }
    }
}
