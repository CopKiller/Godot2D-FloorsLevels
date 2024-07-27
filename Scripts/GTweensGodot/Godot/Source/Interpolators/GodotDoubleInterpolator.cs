using GodotFloorLevels.Scripts.GTweensGodot.GTweens.Source.Easings;
using GodotFloorLevels.Scripts.GTweensGodot.GTweens.Source.Interpolators;

namespace GodotFloorLevels.Scripts.GTweensGodot.Godot.Source.Interpolators;

public sealed class GodotDoubleInterpolator : IInterpolator<double>
{
    public static readonly GodotDoubleInterpolator Instance = new();

    GodotDoubleInterpolator()
    {

    }

    public double Evaluate(
        double initialValue, 
        double finalValue, 
        float time, 
        EasingDelegate easingDelegate
    )
    {
        return easingDelegate((float)initialValue, (float)finalValue, time);
    }

    public double Subtract(double initialValue, double finalValue)
    {
        return finalValue - initialValue;
    }

    public double Add(double initialValue, double finalValue)
    {
        return finalValue + initialValue;
    }
}