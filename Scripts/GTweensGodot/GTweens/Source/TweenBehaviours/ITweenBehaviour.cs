using GodotFloorLevels.Scripts.GTweensGodot.GTweens.Source.Easings;
using GodotFloorLevels.Scripts.GTweensGodot.GTweens.Source.Enums;

namespace GodotFloorLevels.Scripts.GTweensGodot.GTweens.Source.TweenBehaviours;

public interface ITweenBehaviour
{
    float GetDuration();
    float GetElapsed();
    float GetRemaining();
    bool GetLoopable();
    
    void Start(bool isCompletingInstantly);
    void Tick(float deltaTime);
    void Kill();
    void Complete();
    void Reset(bool kill, ResetMode loopResetMode);

    void SetEasing(EasingDelegate easingFunction);
    
    bool GetFinished();
}