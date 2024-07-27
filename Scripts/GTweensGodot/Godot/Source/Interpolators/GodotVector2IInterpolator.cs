using Godot;
using GodotFloorLevels.Scripts.GTweensGodot.GTweens.Source.Easings;
using GodotFloorLevels.Scripts.GTweensGodot.GTweens.Source.Interpolators;

namespace GodotFloorLevels.Scripts.GTweensGodot.Godot.Source.Interpolators;

public sealed class GodotVector2IInterpolator : IInterpolator<Vector2I>
{
    public static readonly GodotVector2IInterpolator Instance = new();

    GodotVector2IInterpolator()
    {

    }

    public Vector2I Evaluate(
        Vector2I initialValue, 
        Vector2I finalValue, 
        float time, 
        EasingDelegate easingDelegate
    )
    {
        return new Vector2I(
            (int)easingDelegate(initialValue.X, finalValue.X, time),
            (int)easingDelegate(initialValue.Y, finalValue.Y, time)
        );
    }

    public Vector2I Subtract(Vector2I initialValue, Vector2I finalValue)
    {
        return finalValue - initialValue;
    }

    public Vector2I Add(Vector2I initialValue, Vector2I finalValue)
    {
        return finalValue + initialValue;
    }
}