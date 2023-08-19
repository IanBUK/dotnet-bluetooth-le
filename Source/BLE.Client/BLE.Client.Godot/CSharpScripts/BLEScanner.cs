using System;
using Godot;
//using nexus.protocols.ble;


namespace BLEScan
{
    public class BLEScanner//: IBluetoothLowEnergyAdapter
    {
        public FoundSensorDelegate FoundSensor;
        
        internal void CallFoundSensorCallback(Sensor sensor)
        {
            FoundSensor?.Invoke(sensor);
        }
        
        private static BleLinkBase _link;
        

        public BLEScanner()
        {
            Main.sendDebugMessage("into BLEScanner constructor");
            _link = BleLinkFactory.GetBleLink(BleLinkFactory.BleLinks.Android);
        }

        public bool Configure(Node2D signalOwner)
        {
            Main.sendDebugMessage("into _scanner.Configure");
            return _link.Configure(signalOwner);
        }


        public static void SetFoundSensorCallback(FoundSensorDelegate callback)
        {
            _link.FoundSensor = callback;
        }
        
        private bool _isScanning = false;

        public bool IsScanning()
        {
            return _isScanning;
        }

        public  /*async*/ void Start()
        {
            Main.sendDebugMessage("into .Start");
            if (!_isScanning)
            {
                Main.sendDebugMessage("not already scanning.");
                if (_link == null)
                {
                    Main.sendDebugMessage("scan would fail, _link is null");
                }
                else
                {
                    _isScanning = true;
                    /*await*/ _link.StartScan();
                }
            }
        }

        public void Stop()
        {
            if (_isScanning)
            {
                _link.StopScan();
                _isScanning = false;
            }
        }



        public void InitialiseBLE()
        {
            //BluetoothLowEnergyAdapter.InitialiseBLE(this);
            //_ble = BluetoothLowEnergyAdapter.ObtainDefaultAdapter()
            // if(_ble.AdapterCanBeEnabled && _ble.CurrentState.IsDisabledOrDisabling()) 
            // {
            //     await _ble.EnableAdapter();
            // }
            // var state = _ble.CurrentState.Value; // e.g.: EnabledDisabledState.Enabled
            // // The adapter implements IObservable<EnabledDisabledState> so you can subscribe to its state
        }


        public void Scan()
        {
            // var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            // await _ble.ScanForBroadcasts(
            // // providing ScanSettings is optional
            // new ScanSettings()
            // {
            //     // Setting the scan mode is currently only applicable to Android and has no effect on other platforms.
            //     // If not provided, defaults to ScanMode.Balanced
            //     Mode = ScanMode.LowPower,

            //     // Optional scan filter to ensure that the observer will only receive peripherals
            //     // that pass the filter. If you want to scan for everything around, omit the filter.
            //     Filter = new ScanFilter()
            //     {
            //         AdvertisedDeviceName = "foobar",
            //         AdvertisedManufacturerCompanyId = 76,
            //         // peripherals must advertise at-least-one of any GUIDs in this list
            //         AdvertisedServiceIsInList = new List<Guid>(){ someGuid },
            //     },

            //     // ignore repeated advertisements from the same device during this scan
            //     IgnoreRepeatBroadcasts = false
            // },
            // // Your IObserver<IBlePeripheral> or Action<IBlePeripheral> will be triggered for each discovered
            // // peripheral based on the provided scan settings and filter (if any).
            // ( IBlePeripheral peripheral ) =>
            // {
            //     // read the advertising data
            //     var adv = peripheral.Advertisement;
            //     Debug.WriteLine( adv.DeviceName );
            //     Debug.WriteLine( adv.Services.Select( x => x.ToString() ).Join( "," ) );
            //     Debug.WriteLine( adv.ManufacturerSpecificData.FirstOrDefault().CompanyName() );
            //     Debug.WriteLine( adv.ServiceData );

            //     // if we found what we needed, stop the scan manually
            //     cts.Cancel();

            //     // perhaps connect to the device (see next example)...
            // },
            // // Provide a CancellationToken to stop the scan, or use the overload that takes a TimeSpan.
            // // If you omit this argument, the scan will timeout after BluetoothLowEnergyUtils.DefaultScanTimeout
            // cts.Token
            // );           
        }


    }
}