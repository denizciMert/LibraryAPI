using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAPI.Entities.Models
{
    public class Title : AbstractEntity
    {
        [Column(TypeName = "varchar(100)")]
        public string TitleName { get; set; } = string.Empty;
    }
}
