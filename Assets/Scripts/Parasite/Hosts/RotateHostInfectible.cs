using UnityEngine;

public class RotateHostInfectible : BaseHost
{
    public override void AttemptHostAction()
    {
        transform.Rotate(0, 0, 15f);
    }
}
