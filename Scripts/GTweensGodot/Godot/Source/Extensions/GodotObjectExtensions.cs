using Godot;
using GodotFloorLevels.Scripts.GTweensGodot.GTweens.Source.Delegates;

namespace GodotFloorLevels.Scripts.GTweensGodot.Godot.Source.Extensions;

public static class GodotObjectExtensions
{
    public static ValidationDelegates.Validation GetGodotObjectValidationFunction(GodotObject godotObject) 
        => () => GodotObject.IsInstanceValid(godotObject);
}