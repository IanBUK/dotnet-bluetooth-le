using System.Diagnostics;
using System.Linq;

namespace BLEScan
{
    public static class SensorParser
    {
        const int IndexOrientationPitch = 0;
        const int IndexOrientationRoll = 2;
        const int IndexOrientationYaw = 4;

        const int IndexAccelerationX = 6;
        const int IndexAccelerationY = 8;
        const int IndexAccelerationZ = 10;

        const int IndexGyroscopeX = 12;
        const int IndexGyroscopeY = 14;
        const int IndexGyroscopeZ = 16;      
        const int IndexBattery = 18;
         
        private static double GetDoubleFromByteArray(byte[] message, int offset)
        {
            if (offset >= message.Length)
            {
                Debug.WriteLine($"Error in GetDoubleFromByteArray. offset: {offset}, message length: {message.Length}, message: '{message}'");
                return 0.0F;
            }
            var upperByte = message[offset];
            var lowerByte = message[offset + 1];

            var result = (new double()).FromFloat16(upperByte, lowerByte);
            return result;
        }
        private static double GetDoubleFromByteArray(byte[] message, int offset, int negativeFlagBit, int negativeFlagByte)
        {
            if (offset >= message.Length || negativeFlagByte >= message.Length)
            {
                Debug.WriteLine($"Error in GetDoubleFromByteArray. offset: {offset}, negativeFlagByte: {negativeFlagByte}  message length: {message.Length}, message: '{message}'");
                return 0.0F;
            }
            var upperByte = message[offset];
            var lowerByte = message[offset + 1];

            var result = (new double()).FromFloat16(upperByte, lowerByte);
            if ((message[negativeFlagByte] & negativeFlagBit) != 0)
            {
                result *= -1;
            }

            return result;
        }

        
        //public static Sensor FromBlePeripheral(IBlePeripheral peripheral)
        //{
        //    var sensor = new Sensor
        //    {
        //        Id = peripheral.Advertisement.DeviceName.StartsWith("RHB")
        //            ? peripheral.Advertisement.DeviceName.Substring(3)
        //            : peripheral.Advertisement.DeviceName,
        //        Name = peripheral.Advertisement.DeviceName
        //    };

        //    if (!peripheral.Advertisement.ManufacturerSpecificData.Any()) return sensor;
            
        //    var item = peripheral.Advertisement.ManufacturerSpecificData.First();
                
        //    sensor.Accelerometer.X = GetDoubleFromByteArray(item.Data,  IndexAccelerationX);
        //    sensor.Accelerometer.Y = GetDoubleFromByteArray(item.Data,  IndexAccelerationY);
        //    sensor.Accelerometer.Z = GetDoubleFromByteArray(item.Data,  IndexAccelerationZ);

        //    sensor.GyroScope.X = GetDoubleFromByteArray(item.Data,  IndexGyroscopeX);
        //    sensor.GyroScope.Y = GetDoubleFromByteArray(item.Data,  IndexGyroscopeY);
        //    sensor.GyroScope.Z = GetDoubleFromByteArray(item.Data,  IndexGyroscopeZ);

        //    sensor.Orientation.Pitch = GetDoubleFromByteArray(item.Data,  IndexOrientationPitch);
        //    sensor.Orientation.Roll = GetDoubleFromByteArray(item.Data,  IndexOrientationRoll);
        //    sensor.Orientation.Yaw = GetDoubleFromByteArray(item.Data,  IndexOrientationYaw);
        
        //    sensor.BatteryLevel = (int) item.Data[IndexBattery];

        //    return sensor;
        //}
    }
}