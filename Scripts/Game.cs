using Godot;
using GodotFloorLevels.Scripts.PlayerScripts;
using GodotFloorLevels.Scripts.WorldBase;

namespace GodotFloorLevels.Scripts;

public partial class Game : Node2D
{
	private Node2D _world2D;
	private World _world;

	public override void _Ready()
	{
		InitializeTree();
	}

	private void InitializeTree()
	{
		_world2D = new Node2D { Name = "World2D" };
		AddChild(_world2D);
	}

	public void LoadWorld(int worldId)
	{
		_world = ResourceLoader
			.Load<PackedScene>($"res://Scenes/Worlds/{worldId}/World.tscn")
			.InstantiateOrNull<World>();
		_world2D.AddChild(_world);
	}
}