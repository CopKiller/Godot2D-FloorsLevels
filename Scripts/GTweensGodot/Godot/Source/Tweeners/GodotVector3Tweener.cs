using Godot;
using GodotFloorLevels.Scripts.GTweensGodot.Godot.Source.Interpolators;
using GodotFloorLevels.Scripts.GTweensGodot.GTweens.Source.Delegates;
using GodotFloorLevels.Scripts.GTweensGodot.GTweens.Source.Tweeners;

namespace GodotFloorLevels.Scripts.GTweensGodot.Godot.Source.Tweeners;

public sealed class GodotVector3Tweener : Tweener<Vector3>
{
    public GodotVector3Tweener(
        Getter currentValueGetter,
        Setter setter,
        Getter to,
        float duration,
        ValidationDelegates.Validation validation
    )
        : base(
            currentValueGetter,
            setter,
            to,
            duration,
            GodotVector3Interpolator.Instance, 
            validation
        )
    {
    }
}