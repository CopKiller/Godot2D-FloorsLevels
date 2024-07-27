using System.Numerics;
using GodotFloorLevels.Scripts.GTweensGodot.GTweens.Source.Delegates;
using GodotFloorLevels.Scripts.GTweensGodot.GTweens.Source.Interpolators;

namespace GodotFloorLevels.Scripts.GTweensGodot.GTweens.Source.Tweeners
{
    public sealed class SystemQuaternionTweener : Tweener<Quaternion>
    {
        public SystemQuaternionTweener(
            Getter currValueGetter,
            Setter setter,
            Getter to,
            float duration,
            ValidationDelegates.Validation validation
            )
            : base(
                  currValueGetter,
                  setter,
                  to,
                  duration,
                  SystemQuaternionInterpolator.Instance,
                  validation
                  )
        {
        }
    }
}