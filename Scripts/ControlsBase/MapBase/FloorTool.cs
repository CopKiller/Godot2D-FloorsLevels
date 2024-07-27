using Godot;
using Godot.Collections;
using Array = System.Array;

namespace GodotFloorLevels.Scripts.ControlsBase.MapBase;

public partial class FloorTool : Node2D
{
    [Export] public int CustomRangeVisible { get; set; } = 0;
    
    [Export] public int[] VisibleFloors { get; set; } = Array.Empty<int>();

}