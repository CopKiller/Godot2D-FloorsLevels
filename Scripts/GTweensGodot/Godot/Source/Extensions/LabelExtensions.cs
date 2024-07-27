using Godot;
using GodotFloorLevels.Scripts.GTweensGodot.GTweens.Source.Extensions;
using GodotFloorLevels.Scripts.GTweensGodot.GTweens.Source.Tweens;

namespace GodotFloorLevels.Scripts.GTweensGodot.Godot.Source.Extensions;

public static class LabelExtensions
{
    public static GTween TweenDisplayedTextVisibleRatio(this Label target, float value, float duration)
    {
        return GTweenExtensions.Tween(
            () => target.VisibleRatio,
            current => target.VisibleRatio = current,
            value,
            duration,
            GodotObjectExtensions.GetGodotObjectValidationFunction(target)
        );
    }
}