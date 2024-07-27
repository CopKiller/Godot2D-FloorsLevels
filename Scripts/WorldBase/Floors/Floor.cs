using System.Collections.Generic;
using Godot;

namespace GodotFloorLevels.Scripts.WorldBase.Floors;

public class Floor
{
    public int Level { get; set; }
    public List<TileMapLayer> Objects { get; set; }
    public TileMapLayer DataLayer { get; set; }
    
    private readonly FloorManager _floorManager;

    public Floor(int level, FloorManager floorManager)
    {
        Level = level;
        Objects = new List<TileMapLayer>();
        _floorManager = floorManager;
    }
    public void CheckPlayerPosition(Vector2I position)
    {
        var tileData = DataLayer.GetCellTileData(position);
        
        _floorManager.HideFloorsAbove(position);
        
        if (tileData == null) return;
        
        var floorUp = (bool)tileData.GetCustomData("FloorUp");
        var floorDown = (bool)tileData.GetCustomData("FloorDown");
        
        if (floorUp)
        {
            var signal = _floorManager.EmitSignal(FloorManager.SignalName.FloorGoUp);
        }
        else if (floorDown)
        {
            var signal = _floorManager.EmitSignal(FloorManager.SignalName.FloorGoDown);
        }
        
    }
}
