using Godot;
using GodotFloorLevels.Scripts.ControlsBase.MapBase;
using GodotFloorLevels.Scripts.WorldBase.Floors;

namespace GodotFloorLevels.Scripts.WorldBase;

public partial class World : Node
{
    [Export] public int MinFloor { get; set; }
    [Export] public int MaxFloor { get; set; }
    public FloorManager FloorManager { get; set; }

    public override void _Ready()
    {
        FloorManager = new FloorManager();
        LoadWorld();
    }
    
    private void LoadWorld()
    {
        FloorManager.CurrentFloorLevel = 0;
        
        for (var floorLevel = MinFloor; floorLevel <= MaxFloor; floorLevel++)
        {
            var floorNode = GetNodeOrNull<FloorTool>(floorLevel.ToString());
            if (floorNode == null) continue;
            
            FloorManager.AddFloor(floorLevel, floorNode);

            var layers = floorNode.GetChildren();
            foreach (var layer in layers)
            {
                if (layer is TileMapLayer tileLayer)
                {
                    FloorManager.AddObjectToFloor(floorLevel, tileLayer);
                }
            }
        }
        
        FloorManager.UpdateVisibleLayers();
    }
}

