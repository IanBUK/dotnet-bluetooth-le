using System;
using System.Threading.Tasks;
using Godot;

namespace BLEScan
{
    public class BLELinkAndroid : BleLinkBase
    {

        public override bool Configure(Node2D signalOwner)
        {
            return false;
        }


        public const Int32 SCAN_SECONDS_MAX = 600;

        public static Double ClampSeconds(Double seconds)
        {
            return Math.Max(Math.Min(seconds, SCAN_SECONDS_MAX), 0);
        }

        public Int32 ScanTimeRemaining => (Int32) ClampSeconds((_scanStopTime - DateTime.UtcNow).TotalSeconds);


        private DateTime _scanStopTime;

        private void sendDebugMessage(string message)
        {
            Main.sendDebugMessage(message);
        }
        
        
        
        public BLELinkAndroid()
        {
            sendDebugMessage("into BLELinkAndroid constructor");
        }

        public new FoundSensorDelegate FoundSensor;

        public new PokeSensorDelegate PokeSensor;


        public int LogBleMessage(string message)
        {
            sendDebugMessage(message);
            return 0;
        }
        
        private void StartTimer()
        {
        
        }

        public override Task<int> StartScan()
        {
            StartTimer();
            return Task.FromResult(0);
        }
        
    }
}