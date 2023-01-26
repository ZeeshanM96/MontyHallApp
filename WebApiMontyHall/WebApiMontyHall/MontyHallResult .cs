using System.Numerics;

namespace WebApiMontyHall
{
    public class MontyHallResult
    {
        public string UserSelectedDoor { get; set; }
        public string HostOpenedDoor { get; set; }

        public string CarDoor { get; set; }
        public bool ShouldSwitch { get; set; }
        public string Result { get; set; }
    }
   
}