using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAPI.Entities.DTOs.AddressDTO
{
    public class AddressPost
    {
        public string AddressString { get; set; } = string.Empty;
        public int DistrictId { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}
