using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakerMovieMockaroo.Models
{
    public class MockarooMovieRequest
    {
        public List<MovieField> Fields { get; set; }
    }

    public class MovieField
    {
        public string name { get; set; }
        public string type { get; set; }
        public string min { get; set; }
        public string max { get; set; }
        public string format { get; set; }
        public string symbol { get; set; }
        public int decimals { get; set; }
    }
}
