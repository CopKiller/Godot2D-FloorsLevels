using GodotFloorLevels.Scripts.GTweensGodot.GTweens.Source.Delegates;

namespace GodotFloorLevels.Scripts.GTweensGodot.GTweens.Source.Extensions;

public static class ValidationExtensions
{
    public static readonly ValidationDelegates.Validation AlwaysValid = () => true;
}