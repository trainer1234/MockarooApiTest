using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakerMovieMockaroo.Models
{
    public class MockarooMovieRequest
    {
        public int Count { get; set; }
        public bool AlwaysArray { get; set; }
        public List<Field> Movies { get; set; }
    }

    public class Field
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Min { get; set; }
        public string Max { get; set; }
        public string Symbol { get; set; }
        public string Format { get; set; }
    }
}
