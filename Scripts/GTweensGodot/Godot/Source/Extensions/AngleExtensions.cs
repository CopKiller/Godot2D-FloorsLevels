using Godot;
using GodotFloorLevels.Scripts.GTweensGodot.GTweens.Source.Enums;

namespace GodotFloorLevels.Scripts.GTweensGodot.Godot.Source.Extensions;

public static class AngleExtensions
{
    public static Vector3 Clamp360(Vector3 eulerAngles)
    {
        return new Vector3(
            GTweens.Source.Extensions.AngleExtensions.Clamp360(eulerAngles.X), 
            GTweens.Source.Extensions.AngleExtensions.Clamp360(eulerAngles.Y), 
            GTweens.Source.Extensions.AngleExtensions.Clamp360(eulerAngles.Z)
        );
    }
    
    public static Vector3 DeltaAngle(Vector3 current, Vector3 target)
    {
        return new Vector3(
            GTweens.Source.Extensions.AngleExtensions.DeltaAngle(current.X, target.X), 
            GTweens.Source.Extensions.AngleExtensions.DeltaAngle(current.Y, target.Y), 
            GTweens.Source.Extensions.AngleExtensions.DeltaAngle(current.Z, target.Z)
        );
    }
    
    public static Vector3 GetDestinationAngleDegrees(Vector3 origin, Vector3 destination, RotationMode mode)
    {
        switch (mode)
        {
            case RotationMode.ShortestDistance:
            {
                Vector3 clampedOrigin = Clamp360(origin);
                Vector3 clampedDestination = Clamp360(destination);
        
                Vector3 deltaAngle = DeltaAngle(clampedOrigin, clampedDestination);
        
                return origin + deltaAngle;
            }
        
            default:
            case RotationMode.TotalDistance:
            {
                return destination;
            }
        }
    }
    
    public static Vector3 GetDestinationAngleRadiants(Vector3 origin, Vector3 destination, RotationMode mode)
    {
        return GetDestinationAngleDegrees(
            origin * MathExtensions.Rad2Deg,
            destination * MathExtensions.Rad2Deg,
            mode
        ) * MathExtensions.Deg2Rad;
    }
}