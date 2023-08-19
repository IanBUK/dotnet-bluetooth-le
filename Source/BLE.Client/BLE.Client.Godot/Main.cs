using Godot;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using BLEScan;


public class Main : Node2D
{

    private BLEScanner _scanner;
    private ItemList _lst;
    private Godot.Color _selectedRowBG = new Godot.Color(0, 0, 1);
    private Godot.Color _unselectedRowBG = new Godot.Color("27252c00");
    private int _maxColumns = 0;

    private int _selectedRow = -1;

    private int _cellCount = 0;

    private static Godot.Color fromRGB(int red, int green, int blue)
    {
        return new Godot.Color((float)red / 255.0F, (float)green / 255.0F, (float)blue / 255.0F);
    }

    public void ping()
    {
        OS.Alert("PING!!");
    }

    public bool Configure()
    {
        if (_scanner == null)
        {
            CreateScanner();
        }

        if (_scanner == null)
        {
            OS.Alert("scanner failed to instantiate");
        }
        else
        {
            sendDebugMessage("about to call configure");
            _scanner.Configure(this);
        }

        return true;
    }

    internal class SensorMobLink
    {
        public Sensor Sensor;
        //internal SensorButton1 mob;
    }
    private Dictionary<string, SensorMobLink> _sensorsFound = new Dictionary<string, SensorMobLink>();


    public static void sendDebugMessage(string message)
    {
        GD.Print(message);
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //CreateScanner();
        // ping();
        Configure();
    }

    private void CreateScanner()
    {
        sendDebugMessage("into CreateScanner");
        _scanner = new BLEScanner();
        BLEScanner.SetFoundSensorCallback(FoundSensor);
        sendDebugMessage("callback set");
        _lst = GetNode<ItemList>("Panel/listBLEItems");
        _maxColumns = _lst.MaxColumns;
        sendDebugMessage("CreateScanner complete");
    }
    public void FoundSensor(Sensor sensor)
    {
        if (!_sensorsFound.ContainsKey(sensor.Name))
        {
            sendDebugMessage($"found sensor: {sensor.Id}");
            AddSensorToDisplay(sensor);
            _sensorsFound.Add(sensor.Name, new SensorMobLink { Sensor = sensor });
        }
        else
        {
            sendDebugMessage($"updating sensor: {sensor.Id}");
            UpdateSensor(sensor);
        }
    }

    private int FindRowStartForSensor(Sensor sensor)
    {
        if (sensor != null)
        {
            var itemName = sensor.Id;
            var start = 0;
            for (int i = start; i < _lst.Items.Count; i += _maxColumns)
            {
                if (_lst.GetItemText(i) == itemName)
                {
                    return i;
                }
            }
        }

        return -1;
    }

    private void UpdateSensor(Sensor sensor)
    {
        var cell = FindRowStartForSensor(sensor);
        if (cell > -1)
        {
            sendDebugMessage($"  cell: {cell}  set sensor {sensor.Id} battery level to  {sensor.BatteryLevel}%");
            _lst.SetItemText(cell + 2, $"{sensor.BatteryLevel}%");
            _lst.SetItemText(cell + 3, $"({sensor.Accelerometer.X}, {sensor.Accelerometer.Y}, {sensor.Accelerometer.Z})");
        }
        else
        {
            sendDebugMessage($"couldn't find row for {sensor.Id}");
        }
    }

    private void AddSensorToDisplay(Sensor sensor)
    {
        _lst.AddItem(sensor.Id, null, false);
        _cellCount++;
        _lst.SetItemCustomBgColor(_cellCount, _unselectedRowBG);
        _lst.AddItem(sensor.Name, null, false);
        _cellCount++;
        _lst.SetItemCustomBgColor(_cellCount, _unselectedRowBG);
        _lst.AddItem($"{sensor.BatteryLevel}%", null, false);
        _cellCount++;
        _lst.SetItemCustomBgColor(_cellCount, _unselectedRowBG);
        _lst.AddItem($"({sensor.Accelerometer.X}, {sensor.Accelerometer.Y}, {sensor.Accelerometer.Z})", null, false);
        _cellCount++;
        _lst.SetItemCustomBgColor(_cellCount, _unselectedRowBG);
    }

    public void ClearPressed()
    {
        _lst.Items.Clear();
    }

    public void ScanButtonToggled(bool button_pressed)
    {
        if (button_pressed)
        {
            sendDebugMessage("about to start scanning...XXX");
            if (_scanner == null)
            {
                sendDebugMessage("scanner was null");
                CreateScanner();
            }
            sendDebugMessage("_scanner set, let's scan.");
            _scanner.Start();
        }
        else
        {
            sendDebugMessage("About to stop scanning ");
            _scanner.Stop();
        }
    }

    public void _on_listBLEItems_item_selected(int index)
    {
        var rowStart = CellToRowStart(index);
        sendDebugMessage($"rowStart: {rowStart}, _selectedRow: {_selectedRow}");
        if (_selectedRow > -1)
        {
            sendDebugMessage($"unselect row: {rowStart}");
            highlightRow(_selectedRow, false);
        }
        highlightRow(rowStart, true);
        _selectedRow = rowStart;
        sendDebugMessage($"_selectedRow: {_selectedRow}");
    }

    private int CellToRowStart(int index)
    {
        sendDebugMessage($"CellToRowStart: index: {index}");
        var rowStart = (index / _maxColumns) * _maxColumns;
        return rowStart;
    }

    private void highlightRow(int rowStart, bool selected)
    {
        Godot.Color rowColor = selected ? _selectedRowBG : _unselectedRowBG;

        sendDebugMessage($"row: {rowStart}, maxCols: {_maxColumns}");
        for (int i = 0; i < _maxColumns; i++)
        {

            _lst.SetItemCustomBgColor(rowStart + i, rowColor);
        }
    }


    private void OnCharacteristicRead(object data)
    {
        sendDebugMessage($"OnCharacteristicRead: ");
    }

    private void OnCharacteristicFinding(object data)
    {
        sendDebugMessage($"OnCharacteristicFinding:  ");
    }

    private void OnCharacteristicFound(object data)
    {
        sendDebugMessage($"OnCharacteristicFound:  ");
    }

    private void OnConnectionStatusChange(object data)
    {
        sendDebugMessage($"OnConnectionStatusChange:  ");
    }

    private void OnLocationStatusChange(object data)
    {
        sendDebugMessage($"OnLocationStatusChange:  ");
    }

    private void OnBluetoothStatusChange(object data)
    {
        sendDebugMessage($"OnBluetoothStatusChange:  ");
    }

    private void OnDeviceFound(object data)
    {

        sendDebugMessage($"OnDeviceFound: '{data.ToString()}', '{data.GetType().Namespace}'.>>.'{data.GetType().FullName}'");
        var dictionary = data as Godot.Collections.Dictionary;
        if (dictionary != null)
        {
            var keys = dictionary.Keys;
            foreach (var key in keys)
            {
                var val = dictionary[key];
                sendDebugMessage($"    {key}: {val}");
            }
        }
    }

    private void OnDebugMessage(object data)
    {
        sendDebugMessage($"OnDebugMessage:  '{data.ToString()}'");
    }
}