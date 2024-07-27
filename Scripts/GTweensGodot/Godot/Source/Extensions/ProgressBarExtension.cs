using Godot;
using GodotFloorLevels.Scripts.GTweensGodot.GTweens.Source.Tweens;

namespace GodotFloorLevels.Scripts.GTweensGodot.Godot.Source.Extensions;

public static class ProgressBarExtension
{
    public static GTween TweenProgressBarValue(this ProgressBar target, double to, float duration)
    {
        return GTweenGodotExtensions.Tween(
            () => target.Value,
            current => target.Value = current, 
            to, 
            duration,
            GodotObjectExtensions.GetGodotObjectValidationFunction(target)
        );
    }
}