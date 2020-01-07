using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uluru.Models;

namespace Uluru.Data.WorkEntries.DTOs
{
    public class WorkEntriesDTO
    {
        public int Quantity { get; set; }
        public WorkEntry WorkEntry { get; set; }
    }
}
