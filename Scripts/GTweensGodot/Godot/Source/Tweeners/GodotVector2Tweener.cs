using Godot;
using GodotFloorLevels.Scripts.GTweensGodot.Godot.Source.Interpolators;
using GodotFloorLevels.Scripts.GTweensGodot.GTweens.Source.Delegates;
using GodotFloorLevels.Scripts.GTweensGodot.GTweens.Source.Tweeners;

namespace GodotFloorLevels.Scripts.GTweensGodot.Godot.Source.Tweeners;

public sealed class GodotVector2Tweener : Tweener<Vector2>
{
    public GodotVector2Tweener(
        Getter getter,
        Setter setter,
        Getter to,
        float duration,
        ValidationDelegates.Validation validation
    )
        : base(
            getter,
            setter,
            to,
            duration,
            GodotVector2Interpolator.Instance, 
            validation
        )
    {
    }
}