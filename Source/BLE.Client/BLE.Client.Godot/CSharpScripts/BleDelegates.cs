using System;
namespace BLEScan
{

    public delegate void FoundSensorDelegate(Sensor sensor);


    public delegate Sensor PokeSensorDelegate(string sensorName);
}

