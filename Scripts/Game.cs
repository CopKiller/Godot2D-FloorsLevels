using Godot;
using GodotFloorLevels.Scripts.PlayerScripts;
using GodotFloorLevels.Scripts.WorldBase;

namespace GodotFloorLevels.Scripts;

public partial class Game : Node2D
{
	private Node2D _world2D;
	
	private World _world;

	private Node2D _players;
	
	private Player _player;
	
	// Informações que podem ser atribuidas em carregamento do jogador
	public int WorldId = 0;
	
	public override void _Ready()
	{
		InitializeTree();
		
		LoadWorld();
		
		LoadPlayer();
	}
	
	private void InitializeTree() // Root/Game/World2D/         1-World || 2-Players
	{
		_world2D = new Node2D();
		_world2D.Name = "World2D";
		this.AddChild(_world2D);

		_players = new Node2D();
		_players.Name = "Players";
		_players.ZIndex = 1;
		_players.YSortEnabled = true;
		_world2D.AddChild(_players);
	}

	private void LoadWorld()
	{
		_world = ResourceLoader
			.Load<PackedScene>("res://Scenes/Worlds/" + WorldId + "/World.tscn")
			.InstantiateOrNull<World>();
		_world2D.AddChild(_world);
	}

	private void LoadPlayer()
	{
		_player = ResourceLoader
			.Load<PackedScene>("res://Scenes/Player.tscn")
			.InstantiateOrNull<Player>();
		
		if (WorldId == 0)
		{
			_player.SetPlayerPosition(new Vector2I(10, 10));
		}
		else
		{
			_player.SetPlayerPosition(new Vector2I(0, 0));
		}
		_players.AddChild(_player);
	}
}