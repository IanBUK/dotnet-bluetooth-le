using System;
namespace BLEScan
{
    public class SensorOrientation
    {
        // based on info in: https://howthingsfly.si.edu/flight-dynamics/roll-pitch-and-yaw
        public SensorOrientation()
        {
            Pitch = 0.0F;
            Roll = 0.0F;
            Yaw = 0.0F;
        }
        public double Pitch;
        public double Roll;
        public double Yaw;

        public override string ToString()
        {
            return $"R:{Roll:F)}, P:{Pitch:F}, Y:{Yaw:F)}";
        }
    }
}

