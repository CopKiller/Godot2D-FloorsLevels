using Godot;
using GodotFloorLevels.Scripts.ControlsBase.MapBase;
using GodotFloorLevels.Scripts.PlayerScripts;
using GodotFloorLevels.Scripts.WorldBase.Floors;

namespace GodotFloorLevels.Scripts.WorldBase;

public partial class World : Node
{
    [Export] public int MinFloor { get; set; }
    [Export] public int MaxFloor { get; set; }
    [Export] public int WorldId { get; set; }
    public FloorManager FloorManager { get; private set; }

    public override void _Ready()
    {
        FloorManager = new FloorManager();
        LoadWorld();
        LoadPlayers();
    }

    private void LoadWorld()
    {
        FloorManager.CurrentFloorLevel = 0;

        for (int floorLevel = MinFloor; floorLevel <= MaxFloor; floorLevel++)
        {
            var floorNode = GetNodeOrNull<FloorTool>(floorLevel.ToString());
            if (floorNode == null) continue;

            FloorManager.AddFloor(floorLevel, floorNode);
        }

        FloorManager.UpdateVisibleLayers();
    }

    private void LoadPlayers()
    {
        AddMainPlayer();
        AddRemotePlayers();
    }

    private void AddMainPlayer()
    {
        var player = ResourceLoader
            .Load<PackedScene>("res://Scenes/Player.tscn")
            .InstantiateOrNull<Player>();
        Vector2I startPos = WorldId == 0 ? new Vector2I(10, 10) : new Vector2I(0, 0);
        player.SetPlayerPosition(startPos);
        FloorManager.AddPlayerToFloor(0, player);
    }

    private void AddRemotePlayers()
    {
        var remoteScene = ResourceLoader.Load<PackedScene>("res://Scenes/Remote.tscn");
        for (int i = 0; i < 3; i++)
        {
            var remotePlayer = remoteScene.InstantiateOrNull<Node2D>();
            FloorManager.AddPlayerToFloor(i, remotePlayer);
            remotePlayer.Position = new Vector2((10 + i) * 32, (10 + i) * 32);
        }
    }
}

