
using Godot;

namespace GodotFloorLevels.Scripts;

public partial class Main : Control
{
    private Game _game;
    
    private Button _world1Button;
    private Button _world2Button;
    
    public override void _Ready()
    {
        _game = new Game();
        _game.Name = nameof(Game);
        
        _world1Button = GetNode<Button>("%ButtonWorld0");
        _world2Button = GetNode<Button>("%ButtonWorld1");
        
        AssignButtonSignals();
    }
    
    private void AssignButtonSignals()
    {
        _world1Button.Pressed += () => LoadWorldId(0);
        _world2Button.Pressed += () => LoadWorldId(1);
    }

    private void LoadWorldId(int id)
    {
        _game.WorldId = id;
        
        GetTree().Root.AddChild(_game);
        
        this.QueueFree();
    }
}