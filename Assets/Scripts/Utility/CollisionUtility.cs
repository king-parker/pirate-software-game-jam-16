using UnityEngine;

public class CollisionUtility
{
    public static bool IsInCollisionLayers(int layer, LayerMask checkLayers)
    {
        return ((1 << layer) & checkLayers) != 0;
    }
}
