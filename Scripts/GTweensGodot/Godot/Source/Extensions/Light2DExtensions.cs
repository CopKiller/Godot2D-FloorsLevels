﻿using Godot;
using GodotFloorLevels.Scripts.GTweensGodot.GTweens.Source.Extensions;
using GodotFloorLevels.Scripts.GTweensGodot.GTweens.Source.Tweens;

namespace GodotFloorLevels.Scripts.GTweensGodot.Godot.Source.Extensions;

public static class Light2DExtensions
{
    public static GTween TweenColor(this Light2D target, Color to, float duration)
    {
        return GTweenGodotExtensions.Tween(
            () => target.Color,
            current => target.Color = current, 
            to, 
            duration,
            GodotObjectExtensions.GetGodotObjectValidationFunction(target)
        );
    }
    
    public static GTween TweenEnergy(this Light2D target, float to, float duration)
    {
        return GTweenExtensions.Tween(
            () => target.Energy,
            current => target.Energy = current, 
            to, 
            duration,
            GodotObjectExtensions.GetGodotObjectValidationFunction(target)
        );
    }
    
    public static GTween TweenShadowColor(this Light2D target, Color to, float duration)
    {
        return GTweenGodotExtensions.Tween(
            () => target.ShadowColor,
            current => target.ShadowColor = current, 
            to, 
            duration,
            GodotObjectExtensions.GetGodotObjectValidationFunction(target)
        );
    }
}