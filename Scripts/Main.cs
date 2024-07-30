
using Godot;

namespace GodotFloorLevels.Scripts;

public partial class Main : Control
{
    private Game _game;

    public override void _Ready()
    {
        _game = new Game();
        _game.Name = nameof(Game);

        AssignButtonSignals();
    }

    private void AssignButtonSignals()
    {
        for (int i = 0; i < 2; i++)
        {
            var button = GetNode<Button>($"%ButtonWorld{i}");
            int worldIndex = i; // Captura de variável para lambda
            button.Pressed += () => LoadGameWorld(worldIndex);
        }
    }

    private void LoadGameWorld(int worldId)
    {
        GetTree().Root.AddChild(_game);
        _game.LoadWorld(worldId);
        QueueFree();
    }
}