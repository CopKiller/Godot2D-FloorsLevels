using Godot;
using GodotFloorLevels.Scripts.GTweensGodot.Godot.Source.Interpolators;
using GodotFloorLevels.Scripts.GTweensGodot.GTweens.Source.Delegates;
using GodotFloorLevels.Scripts.GTweensGodot.GTweens.Source.Tweeners;

namespace GodotFloorLevels.Scripts.GTweensGodot.Godot.Source.Tweeners;

public sealed class GodotVector2ITweener : Tweener<Vector2I>
{
    public GodotVector2ITweener(
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
            GodotVector2IInterpolator.Instance, 
            validation
        )
    {
    }
}