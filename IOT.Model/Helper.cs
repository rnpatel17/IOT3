using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Model
{
    public enum Alert
    {
        None = 0,
        Once = 1,
        Multiple = 2
    }
    public enum Effect
    {
        None = 0,
        ColorLoop = 1
    }
    public enum GroupType
    {
        LightGroup = 0,
        Room = 1,
        Luminaire = 2,
        LightSource = 3
    }

    public enum RoomClass
    {
        Other = 0,
        LivingRoom = 1,
        Kitchen = 2,
        Dining = 3,
        Bedroom = 4,
        KidsBedroom = 5,
        Bathroom = 6,
        Nursery = 7,
        Recreation = 8,
        Office = 9,
        Gym = 10,
        Hallway = 11,
        Toilet = 12,
        FrontDoor = 13,
        Garage = 14,
        Terrace = 15,
        Garden = 16,
        Driveway = 17,
        Carport = 18
    }
    
}
