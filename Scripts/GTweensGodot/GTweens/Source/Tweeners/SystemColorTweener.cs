using System.Drawing;
using GodotFloorLevels.Scripts.GTweensGodot.GTweens.Source.Delegates;
using GodotFloorLevels.Scripts.GTweensGodot.GTweens.Source.Interpolators;

namespace GodotFloorLevels.Scripts.GTweensGodot.GTweens.Source.Tweeners
{
    public sealed class SystemColorTweener : Tweener<Color>
    {
        public SystemColorTweener(
            Getter currValueGetter, 
            Setter setter, 
            Getter to, 
            float duration, 
            ValidationDelegates.Validation validation
            )
            : base(currValueGetter, 
                  setter, 
                  to, 
                  duration,
                  SystemColorInterpolator.Instance, 
                  validation
                  )
        {
        }
    }
}