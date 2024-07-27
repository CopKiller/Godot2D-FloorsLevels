using GodotFloorLevels.Scripts.GTweensGodot.GTweens.Source.Delegates;
using GodotFloorLevels.Scripts.GTweensGodot.GTweens.Source.Interpolators;

namespace GodotFloorLevels.Scripts.GTweensGodot.GTweens.Source.Tweeners;
    
public sealed class FloatTweener : Tweener<float>
{
    public FloatTweener(
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
            FloatInterpolator.Instance, 
            validation
        )
    {
    }
}