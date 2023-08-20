using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Godot;
using Plugin.BLE;
using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using Plugin.BLE.Abstractions.Extensions;

namespace BLEScan
{
    public class BLEPluginBLE : BleLinkBase
    { 
        private IBluetoothLE _bluetoothManager;
        protected IAdapter Adapter;
        CancellationTokenSource _scanCancellationTokenSource = new();
        CancellationToken _scanCancellationToken;
        private ObservableCollection<Sensor> _devices = new ObservableCollection<Sensor>();
        public IList<Sensor> BLEDevices { get { DebugMessage("Getting BLEDevices"); return _devices; } }

        public BLEPluginBLE()
		{
            _scanCancellationToken = _scanCancellationTokenSource.Token;
        }

        public override bool Configure(Node2D signalOwner)
        {
            return true;
        }


        /*private void PerformScanForDevices()
        {
            if (!IsScanning)
            {
                IsScanning = true;
                DebugMessage($"Starting Scanning");
                StartScanForDevices();
                DebugMessage($"Started Scan");
            }
            else
            {
                DebugMessage($"Stopping Scan");
                _scanCancellationTokenSource.Cancel();
                IsScanning = false;
                DebugMessage($"Stop Scanning");
            }
        }*/

        private async Task UpdateConnectedDevices()
        {
            foreach (var connectedDevice in Adapter.ConnectedDevices)
            {
                //update rssi for already connected devices (so tha 0 is not shown in the list)
                try
                {
                    await connectedDevice.UpdateRssiAsync();
                }
                catch (Exception ex)
                {
                    ShowMessage($"Failed to update RSSI for {connectedDevice.Name}. Error: {ex.Message}");
                }

                AddOrUpdateDevice(connectedDevice);
            }
        }


        private string _lockAlreadyScanning = string.Empty;

        private bool AlreadyScanning
        {
            get
            {
                lock(_lockAlreadyScanning)
                {
                    return IsScanning;
                }
            }
        }

        private void SetScanning(bool isScanning)
        {
            lock (_lockAlreadyScanning)
            {
                IsScanning = IsScanning;
            }
        }


        public override async Task<int> StartScan()
        {
            if (AlreadyScanning)
            {
                DebugMessage("duplicate scanning call");
            }
            else
            { 
                SetScanning(true);
                DebugMessage("into StartScanForDevices");

                DebugMessage("StartScanForDevices called");
                BLEDevices.Clear();
                DebugMessage("await UpdateConnectedDevices();");
                //UpdateConnectedDevices();
                DebugMessage("_scanCancellationTokenSource = new CancellationTokenSource();");
                _scanCancellationTokenSource = new CancellationTokenSource();
                DebugMessage("got cancellation token");
                try
                {
                    if (Adapter == null)
                    {
                        DebugMessage("Adapter is null!");
                    }
                    Adapter.ScanMode = ScanMode.LowLatency;
                }
                catch (Exception e)
                {
                    DebugMessage($"exception setting scanMode: '{e.Message}'");
                }
                DebugMessage("set scanMode to lowLatency");
                Adapter.DeviceDiscovered -= OnDeviceDiscovered;
                Adapter.DeviceAdvertised -= OnDeviceDiscovered;
                Adapter.ScanTimeoutElapsed -= Adapter_ScanTimeoutElapsed;

                Adapter.DeviceDiscovered += OnDeviceDiscovered;
                Adapter.DeviceAdvertised += OnDeviceDiscovered;
                Adapter.ScanTimeoutElapsed += Adapter_ScanTimeoutElapsed;
                DebugMessage("event handlers refreshed");

                //Adapter.ScanMode = ScanMode.LowLatency;

                DebugMessage("call Adapter.StartScanningForDevicesAsync");
                
                var scanTask = Adapter.StartScanningForDevicesAsync(_scanCancellationTokenSource.Token);
                scanTask.Start();

                DebugMessage("back from Adapter.StartScanningForDevicesAsync");
                SetScanning(false);
            }
            return 0;
        }



        private void DebugMessage(string message)
        {
            //Debug.WriteLine(message);
            GD.Print(message);
        }

        private void ShowMessage(string message)
        {
            DebugMessage(message);
            OS.Alert("BLE Scanner", message);
        }


        private void OnStateChanged(object sender, BluetoothStateChangedArgs e)
        {
            DebugMessage("OnStateChanged");
        }

        private void OnDeviceDiscovered(object sender, DeviceEventArgs args)
        {
            DebugMessage("OnDeviceDiscovered");
            AddOrUpdateDevice(args.Device);
            DebugMessage("OnDeviceDiscovered done");
        }

        private void AddOrUpdateDevice(IDevice device)
        {
            DebugMessage($"Device Found: '{device.Id}'");
            var vm = BLEDevices.FirstOrDefault(d => d.Id == device.Id.ToString());
            if (vm != null)
            {
                DebugMessage($"Update Device: {device.Id}");
            }
            else
            {
                DebugMessage($"Add Device: {device.Id}");
                BLEDevices.Add(new Sensor(device));

            }
            DebugMessage($"Device Found: '{device.Id}' done");
        }

        private void Adapter_ScanTimeoutElapsed(object sender, EventArgs e)
        {
            DebugMessage("Adapter_ScanTimeoutElapsed");
            CleanupCancellationToken();
            DebugMessage("Adapter_ScanTimeoutElapsed done");
        }

        private void CleanupCancellationToken()
        {
            DebugMessage("CleanUpCancellationToken");
            _scanCancellationTokenSource.Dispose();
            _scanCancellationTokenSource = null;

            IsScanning = false;
            DebugMessage("CleanUpCancellationToken done");
        }

        private void ConfigureBLE()
        {
            DebugMessage("into ConfigureBLE");
            _bluetoothManager = CrossBluetoothLE.Current;
            DebugMessage("got _bluetoothManager");
            if (_bluetoothManager == null)
            {
                DebugMessage("CrossBluetoothLE.Current is null");
            }
            else
            {
                _bluetoothManager.StateChanged += OnStateChanged;
            }

            Adapter = CrossBluetoothLE.Current.Adapter;
            if (Adapter == null)
            {
                DebugMessage("CrossBluetoothLE.Current.Adapter is null");
            }
            else
            {
                DebugMessage("go and set event handlers");
                Adapter.DeviceDiscovered += OnDeviceDiscovered;
                Adapter.DeviceAdvertised += OnDeviceDiscovered;
                Adapter.ScanTimeoutElapsed += Adapter_ScanTimeoutElapsed;
                Adapter.ScanMode = ScanMode.LowLatency;
                DebugMessage("event handlers set");
            }

            if (_bluetoothManager == null && Adapter == null)
            {
                ShowMessage("Bluetooth and Adapter are both null");
            }
            else if (_bluetoothManager == null)
            {
                ShowMessage("Bluetooth is null");
            }
            else if (Adapter == null)
            {
                ShowMessage("Adapter is null");
            }
        }
    }
}

