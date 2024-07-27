using Godot;
using GodotFloorLevels.Scripts.GTweensGodot.GTweens.Source.Extensions;
using GodotFloorLevels.Scripts.GTweensGodot.GTweens.Source.Tweens;

namespace GodotFloorLevels.Scripts.GTweensGodot.Godot.Source.Extensions;

public static class AudioStreamPlayer2DExtensions
{
    public static GTween TweenVolumeDb(this AudioStreamPlayer2D target, float to, float duration)
    {
        return GTweenExtensions.Tween(
            () => target.VolumeDb,
            current => target.VolumeDb = current, 
            to, 
            duration,
            GodotObjectExtensions.GetGodotObjectValidationFunction(target)
        );
    }
    
    public static GTween TweenPitchScale(this AudioStreamPlayer2D target, float to, float duration)
    {
        return GTweenExtensions.Tween(
            () => target.PitchScale,
            current => target.PitchScale = current, 
            to, 
            duration,
            GodotObjectExtensions.GetGodotObjectValidationFunction(target)
        );
    }
}