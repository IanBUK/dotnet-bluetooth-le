using System;
using System.Threading.Tasks;
using Godot;

namespace BLEScan
{
    public class BLELinkiOS: BleLinkBase
    {
        public BLELinkiOS()
        {
        }

        public override bool Configure(Node2D signalOwner)
        {
            return false;
        }

        public new FoundSensorDelegate FoundSensor;

        public new PokeSensorDelegate PokeSensor;

        public override async Task<int> StartScan()
        {
            return await Task.FromResult(0);
        }

        public override void StopScan()
        {
            throw new NotImplementedException();
            
        }
    }
}

