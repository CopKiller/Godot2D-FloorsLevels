using System.Collections.Generic;
using Godot;
using GodotFloorLevels.Scripts.ControlsBase.MapBase;

namespace GodotFloorLevels.Scripts.WorldBase.Floors;

public class Floor
{
    public int Level { get; set; }
    public List<TileMapLayer> Objects { get; set; }
    public TileMapLayer DataLayer { get; set; }
    
    private readonly FloorManager _floorManager;
    public readonly FloorTool FloorTool;

    public Floor(int level, FloorManager floorManager, FloorTool floorTool)
    {
        Level = level;
        Objects = new List<TileMapLayer>();
        _floorManager = floorManager;
        FloorTool = floorTool;
    }

    public void CheckPlayerPosition(Vector2I position, Node2D player)
    {
        var tileData = DataLayer.GetCellTileData(position);
        _floorManager.HideFloorsAbove(position);
        
        if (tileData == null) return;
        
        var floorUp = (bool)tileData.GetCustomData("FloorUp");
        var floorDown = (bool)tileData.GetCustomData("FloorDown");
        
        if (floorUp)
        {
            _floorManager.EmitSignal(FloorManager.SignalName.FloorGoUp);
            _floorManager.AddPlayerToFloor(_floorManager.CurrentFloorLevel, player);
        }
        else if (floorDown)
        {
            _floorManager.EmitSignal(FloorManager.SignalName.FloorGoDown);
            _floorManager.AddPlayerToFloor(_floorManager.CurrentFloorLevel, player);
        }
        
    }
}
