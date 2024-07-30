using System;
using Godot;
using Godot.Collections;
using Array = System.Array;

namespace GodotFloorLevels.Scripts.ControlsBase.MapBase;

// Cada nível de piso herda de FloorTool
public partial class FloorTool : Node2D
{
    [Export] public int CustomRangeVisible { get; set; } = 0;
    
    [Export] public int[] VisibleFloors { get; set; } = Array.Empty<int>();
    
    public Node2D Players;

    public override void _Ready()
    {
        Players = new Node2D();
        Players.Name = "Players";
        Players.YSortEnabled = true;
        var inteiro = Convert.ToInt32(this.Name);
        Players.ZIndex = 0;
        AddChild(Players);
    }
    
    public void AddPlayer(Node2D player)
    {
        Players.AddChild(player);
    }
    
    
}