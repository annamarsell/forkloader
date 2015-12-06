using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForkLoader
{
    public class ForkKey
    {
        public int TeamNumber { get; set; }
        public int ClassId { get; set; }
        public List<string> Forks { get; set; }
    }
}
