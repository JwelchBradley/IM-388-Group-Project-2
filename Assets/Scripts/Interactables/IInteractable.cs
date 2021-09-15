using UnityEngine;

public interface IInteractable
{
    public void HighlightObject();

    public void UnHighlightObject();

    public void DisplayObjectName();

    public void RemoveObjectName();

    public void Pickup(GameObject pickUpDest);

    public void Drop();

    public void Equip();
}
