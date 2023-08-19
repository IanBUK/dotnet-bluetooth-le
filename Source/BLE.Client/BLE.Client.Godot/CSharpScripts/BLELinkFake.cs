using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;

namespace BLEScan
{
    public class BLELinkFake: BleLinkBase
    {
        public BLELinkFake()
        {
            //GD.Print("Creating FAKE instance");
            CreateSensorList();
        }

        private int item =0;

        List<Sensor> _fakeSensors = new List<Sensor>();

        System.Threading.Thread _thread;

        private void CreateSensorList()
        {
            _fakeSensors.Add(
                new Sensor{
                    Name = "000",
                    Id = "RHB000",
                    Accelerometer = new Vector3D{ X = 1, Y = 2, Z =3},
                    Magnetometer = new Vector3D{ X = 4, Y = 5, Z =6},
                    GyroScope = new Vector3D{ X = 7, Y = 8, Z =9},
                    Orientation = new SensorOrientation{Roll = -1, Yaw = -2, Pitch = -3}
                }
            );

            _fakeSensors.Add(
                new Sensor{
                    Name = "001",
                    Id = "RHB001",
                    Accelerometer = new Vector3D{ X = 10, Y = 11, Z =12},
                    Magnetometer = new Vector3D{ X = 13, Y = 14, Z =15},
                    GyroScope = new Vector3D{ X = 16, Y = 17, Z =18},
                    Orientation = new  SensorOrientation{Roll = -10, Yaw = -11, Pitch = -12}
                }
            );


            _fakeSensors.Add(
                new Sensor{
                    Name = "002",
                    Id = "RHB002",
                    Accelerometer = new Vector3D{ X = 20, Y = 21, Z =22},
                    Magnetometer = new Vector3D{ X = 23, Y = 24, Z =25},
                    GyroScope = new Vector3D{ X = 26, Y = 27, Z =28},
                    Orientation = new  SensorOrientation{Roll = -20, Yaw = -21, Pitch = -22}
                }
            );


            _fakeSensors.Add(
                new Sensor{
                    Name = "003",
                    Id = "RHB003",
                    Accelerometer = new Vector3D{ X = 30, Y = 31, Z =32},
                    Magnetometer = new Vector3D{ X = 33, Y = 34, Z =35},
                    GyroScope = new Vector3D{ X = 36, Y = 37, Z =38},
                    Orientation = new  SensorOrientation{Roll = -30, Yaw = -31, Pitch = -32}
                }
            );


            _fakeSensors.Add(
                new Sensor{
                    Name = "004",
                    Id = "RHB004",
                    Accelerometer = new Vector3D{ X = 40, Y = 41, Z =42},
                    Magnetometer = new Vector3D{ X = 43, Y = 44, Z =45},
                    GyroScope = new Vector3D{ X = 46, Y = 47, Z =48},
                    Orientation = new  SensorOrientation{Roll = -40, Yaw = -41, Pitch = -42}
                }
            );


            _fakeSensors.Add(
                new Sensor{
                    Name = "005",
                    Id = "RHB005",
                    Accelerometer = new Vector3D{ X = 50, Y = 51, Z =52},
                    Magnetometer = new Vector3D{ X = 53, Y = 54, Z =55},
                    GyroScope = new Vector3D{ X = 56, Y = 57, Z =58},
                    Orientation = new  SensorOrientation{Roll = -50, Yaw = -51, Pitch = -52}
                }
            );
 
            _fakeSensors.Add(
                new Sensor{
                    Name = "006",
                    Id = "RHB006",
                    Accelerometer = new Vector3D{ X = 50, Y = 51, Z =52},
                    Magnetometer = new Vector3D{ X = 53, Y = 54, Z =55},
                    GyroScope = new Vector3D{ X = 56, Y = 57, Z =58},
                    Orientation = new  SensorOrientation{Roll = -50, Yaw = -51, Pitch = -52}
                }
            );
            _fakeSensors.Add(
                new Sensor{
                    Name = "007",
                    Id = "RHB007",
                    Accelerometer = new Vector3D{ X = 50, Y = 51, Z =52},
                    Magnetometer = new Vector3D{ X = 53, Y = 54, Z =55},
                    GyroScope = new Vector3D{ X = 56, Y = 57, Z =58},
                    Orientation = new  SensorOrientation{Roll = -50, Yaw = -51, Pitch = -52}
                }
            );
            _fakeSensors.Add(
                new Sensor{
                    Name = "008",
                    Id = "RHB008",
                    Accelerometer = new Vector3D{ X = 50, Y = 51, Z =52},
                    Magnetometer = new Vector3D{ X = 53, Y = 54, Z =55},
                    GyroScope = new Vector3D{ X = 56, Y = 57, Z =58},
                    Orientation = new  SensorOrientation{Roll = -50, Yaw = -51, Pitch = -52}
                }
            );
            _fakeSensors.Add(
                new Sensor{
                    Name = "009",
                    Id = "RHB009",
                    Accelerometer = new Vector3D{ X = 50, Y = 51, Z =52},
                    Magnetometer = new Vector3D{ X = 53, Y = 54, Z =55},
                    GyroScope = new Vector3D{ X = 56, Y = 57, Z =58},
                    Orientation = new  SensorOrientation{Roll = -50, Yaw = -51, Pitch = -52}
                }
            );
            _fakeSensors.Add(
                new Sensor{
                    Name = "0010",
                    Id = "RHB010",
                    Accelerometer = new Vector3D{ X = 50, Y = 51, Z =52},
                    Magnetometer = new Vector3D{ X = 53, Y = 54, Z =55},
                    GyroScope = new Vector3D{ X = 56, Y = 57, Z =58},
                    Orientation = new  SensorOrientation{Roll = -50, Yaw = -51, Pitch = -52}
                }
            );
            _fakeSensors.Add(
                new Sensor{
                    Name = "011",
                    Id = "RHB011",
                    Accelerometer = new Vector3D{ X = 50, Y = 51, Z =52},
                    Magnetometer = new Vector3D{ X = 53, Y = 54, Z =55},
                    GyroScope = new Vector3D{ X = 56, Y = 57, Z =58},
                    Orientation = new  SensorOrientation{Roll = -50, Yaw = -51, Pitch = -52}
                }
            );
            _fakeSensors.Add(
                new Sensor{
                    Name = "012",
                    Id = "RHB012",
                    Accelerometer = new Vector3D{ X = 50, Y = 51, Z =52},
                    Magnetometer = new Vector3D{ X = 53, Y = 54, Z =55},
                    GyroScope = new Vector3D(){ X = 56, Y = 57, Z =58},
                    Orientation = new  SensorOrientation{Roll = -50, Yaw = -51, Pitch = -52}
                }
            );       
        }

        private void SendRandomSensor()
        {
            while(true)
            {
                var max = _fakeSensors.Count+1;
                var index = new Random().Next(-1,max);
                if (index > _fakeSensors.Count -1)
                {
                    index = _fakeSensors.Count - 1;
                }
                if (index < 0)
                {
                    index = 0;
                }


                //GD.Print($"send sensor no {item}: {_fakeSensors[item].Name}"); 
                _fakeSensors[index].BatteryLevel = new Random().Next(0,99);

                CallFoundSensorCallback(_fakeSensors[index]);
                var delay = new Random().Next(20,250);
                System.Threading.Thread.Sleep(delay);

                item++;
                if (item == _fakeSensors.Count)
                {
                    item = 0;
                }
            }
        }

        public new FoundSensorDelegate FoundSensor;

        public new PokeSensorDelegate PokeSensor;


        public override Task<int> StartScan()
        {
            if (_thread == null || !_thread.IsAlive)
            {
                _thread = new System.Threading.Thread(SendRandomSensor);
                _thread.Start();
            }

            return Task.FromResult(0);
        }

        public override void StopScan()
        {
            if (_thread.IsAlive)
            {
                _thread.Abort();
            }
        }

        public override bool Configure(Node2D signalOwner)
        {
            throw new NotImplementedException();
        }
    }
}