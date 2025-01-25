using UnityEngine;

public class HandScanner : MonoBehaviour, IScientistInteractable
{
    [SerializeField] private SlidingDoor controlledDoor;

    public void Interact()
    {
        controlledDoor.OpenCloseDoor();
    }
}
