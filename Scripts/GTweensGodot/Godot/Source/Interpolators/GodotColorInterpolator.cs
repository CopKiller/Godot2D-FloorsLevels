using Godot;
using GodotFloorLevels.Scripts.GTweensGodot.GTweens.Source.Easings;
using GodotFloorLevels.Scripts.GTweensGodot.GTweens.Source.Interpolators;

namespace GodotFloorLevels.Scripts.GTweensGodot.Godot.Source.Interpolators;

public sealed class GodotColorInterpolator : IInterpolator<Color>
{
    public static readonly GodotColorInterpolator Instance = new();

    GodotColorInterpolator()
    {

    }

    public Color Evaluate(
        Color initialValue, 
        Color finalValue, 
        float time, 
        EasingDelegate easingDelegate
    )
    {
        return new Color(
            easingDelegate(initialValue.R, finalValue.R, time),
            easingDelegate(initialValue.G, finalValue.G, time),
            easingDelegate(initialValue.B, finalValue.B, time),
            easingDelegate!(initialValue.A, finalValue.A, time)
        );
    }

    public Color Subtract(Color initialValue, Color finalValue)
    {
        return new Color(
            finalValue.R - initialValue.R,
            finalValue.G - initialValue.G,
            finalValue.B - initialValue.B,
            finalValue.A - initialValue.A
        );
    }

    public Color Add(Color initialValue, Color finalValue)
    {
        return new Color(
            finalValue.R + initialValue.R,
            finalValue.G + initialValue.G,
            finalValue.B + initialValue.B,
            finalValue.A + initialValue.A
        );
    }
}