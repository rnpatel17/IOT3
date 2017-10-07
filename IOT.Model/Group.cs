using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Model
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GroupType? Type { get; set; }
        public RoomClass? Class { get; set; }
        public string ModelId { get; set; }
        public List<string> Lights { get; set; }
        public int StateId { get; set; }
        public State State { get; set; }
        public int GroupStateId { get; set; }
        public GroupState GroupState { get; set; }
    }
}
