﻿using Godot;
using GodotFloorLevels.Scripts.GTweensGodot.GTweens.Source.Extensions;
using GodotFloorLevels.Scripts.GTweensGodot.GTweens.Source.Tweens;

namespace GodotFloorLevels.Scripts.GTweensGodot.Godot.Source.Extensions;

public static class Camera3DExtensions
{
    public static GTween TweenHOffset(this Camera3D target, float to, float duration)
    {
        return GTweenExtensions.Tween(
            () => target.HOffset,
            current => target.HOffset = current, 
            to, 
            duration,
            GodotObjectExtensions.GetGodotObjectValidationFunction(target)
        );
    }
    
    public static GTween TweenVOffset(this Camera3D target, float to, float duration)
    {
        return GTweenExtensions.Tween(
            () => target.VOffset,
            current => target.VOffset = current, 
            to, 
            duration,
            GodotObjectExtensions.GetGodotObjectValidationFunction(target)
        );
    }
    
    public static GTween TweenFov(this Camera3D target, float to, float duration)
    {
        return GTweenExtensions.Tween(
            () => target.Fov,
            current => target.Fov = current, 
            to, 
            duration,
            GodotObjectExtensions.GetGodotObjectValidationFunction(target)
        );
    }
}