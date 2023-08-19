using Plugin.BLE.Abstractions.Contracts;

namespace BLEScan
{
    public class Sensor
    {
        private IDevice device;

        public Sensor()
        {
            
        }

        public Sensor(IDevice device)
        {
            this.device = device;
        }

        public string Name { get; set; }

        public string Id { get; set; }

        public int BatteryLevel { get; set; }

        public Vector3D Accelerometer { get; set; }

        public Vector3D GyroScope { get; set; }

        public Vector3D Magnetometer { get; set; }

        public SensorOrientation Orientation { get; set; }
        
        
        
        
    }
}