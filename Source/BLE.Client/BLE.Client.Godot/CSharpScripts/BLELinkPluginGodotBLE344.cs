using System;
using System.Threading.Tasks;
using Godot;

namespace BLEScan
{
    public class BLELinkPluginGodotBLE344 : BleLinkBase
    {
        private readonly string pluginName = "GodotBluetooth344";
        private JNISingleton _bleEngine = null;
        private bool _bluetoothStatus = false;
        private bool _locationPermission = false;
        private bool _locationStatus = false;

        private void SendDebugMessage(string message)
        {
            Main.sendDebugMessage(message);
        }

        public override bool Configure(Node2D signalOwner)
        {
            if (Engine.HasSingleton(pluginName))
            {
                _bleEngine = (JNISingleton)Engine.GetSingleton(pluginName);
                _bleEngine.Connect("_on_debug_message", signalOwner, "OnDebugMessage");
                _bleEngine.Connect("_on_device_found", signalOwner, "OnDeviceFound");
                _bleEngine.Connect("_on_bluetooth_status_change", signalOwner, "OnBluetoothStatusChange");
                _bleEngine.Connect("_on_location_status_change", signalOwner, "OnLocationStatusChange");
                _bleEngine.Connect("_on_connection_status_change", signalOwner, "OnConnectionStatusChange");
                _bleEngine.Connect("_on_characteristic_found", signalOwner, "OnCharacteristicFound");
                _bleEngine.Connect("_on_characteristic_finding", signalOwner, "OnCharacteristicFinding");
                _bleEngine.Connect("_on_characteristic_read", signalOwner, "OnCharacteristicRead");
                var hasPermissions = CheckPermissions();
                return hasPermissions;
            }
            else
            {
                OS.Alert("Cannot link to BLE engine.");
            }
            return false;
        }

        private bool CheckPermissions()
        {
            SendDebugMessage("Checking permissions");
            _bluetoothStatus = (bool)_bleEngine.Call("bluetoothStatus", new object[0]); 
            SendDebugMessage($"  bluetooth status: {_bluetoothStatus}");

            _locationStatus = (bool)_bleEngine.Call("locationStatus", new object[0]);
            SendDebugMessage($"  location status: {_locationStatus}");

            _locationPermission = (bool)_bleEngine.Call("hasLocationPermissions", new object[0]);
            SendDebugMessage($"  location permission: {_locationPermission}");
            return _bluetoothStatus && _locationStatus && _locationPermission;
        }

        public override Task<int> StartScan()
        {
            Main.sendDebugMessage("into StartScan");
            try
            {
                _bleEngine.Call("scan", new object[0]);
                //_bleEngine.Call("scan", null);
            }
            catch (Exception e)
            {
                Main.sendDebugMessage($"exception in scan: '{e.Message}'");
            }
            return Task.FromResult(0);
        }

    }
}