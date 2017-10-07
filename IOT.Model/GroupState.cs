using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Model
{
    public class GroupState
    {
        public int Id { get; set; }
        public bool? AnyOn { get; set; }
        public bool? AllOn { get; set; }
    }
}
