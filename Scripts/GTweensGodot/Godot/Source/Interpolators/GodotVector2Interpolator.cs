using Godot;
using GodotFloorLevels.Scripts.GTweensGodot.GTweens.Source.Easings;
using GodotFloorLevels.Scripts.GTweensGodot.GTweens.Source.Interpolators;

namespace GodotFloorLevels.Scripts.GTweensGodot.Godot.Source.Interpolators;

public sealed class GodotVector2Interpolator : IInterpolator<Vector2>
{
    public static readonly GodotVector2Interpolator Instance = new();

    GodotVector2Interpolator()
    {

    }

    public Vector2 Evaluate(
        Vector2 initialValue, 
        Vector2 finalValue, 
        float time, 
        EasingDelegate easingDelegate
    )
    {
        return new Vector2(
            easingDelegate(initialValue.X, finalValue.X, time),
            easingDelegate(initialValue.Y, finalValue.Y, time)
        );
    }

    public Vector2 Subtract(Vector2 initialValue, Vector2 finalValue)
    {
        return finalValue - initialValue;
    }

    public Vector2 Add(Vector2 initialValue, Vector2 finalValue)
    {
        return finalValue + initialValue;
    }
}