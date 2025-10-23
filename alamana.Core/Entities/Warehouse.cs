using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alamana.Core.Entities
{
    public class Warehouse : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string? Location { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; } = default!;

        public ICollection<Category> Categories { get; set; } = new List<Category>();

    }
}
