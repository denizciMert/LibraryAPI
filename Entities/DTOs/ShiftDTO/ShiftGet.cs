﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAPI.Entities.DTOs.ShiftDTO
{
    public class ShiftGet
    {
        public int? Id { get; set; }
        public string? ShiftType { get; set; } = string.Empty;
        public DateTime? CreatinDateLog { get; set; }
        public DateTime? UpdateDateLog { get; set; }
        public DateTime? DeleteDateLog { get; set; }
        public string? State { get; set; }
    }
}