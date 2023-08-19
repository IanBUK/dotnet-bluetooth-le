using System;
using System.Threading;
using System.Threading.Tasks;
using Godot;

namespace BLEScan
{
    public abstract class BleLinkBase
    {
        protected double Seconds = 100;
        
        protected CancellationTokenSource ScanCancel;
        public Boolean IsScanning { get; protected set; }


        public abstract bool Configure( Node2D signalOwner);


        public virtual void StopScan()
        {
            ScanCancel?.Cancel();
        }

        public abstract Task<int> StartScan();

        public FoundSensorDelegate FoundSensor;

        public PokeSensorDelegate PokeSensor;

        internal void CallFoundSensorCallback(Sensor sensor)
        {
            FoundSensor?.Invoke(sensor);
        }
    }
}