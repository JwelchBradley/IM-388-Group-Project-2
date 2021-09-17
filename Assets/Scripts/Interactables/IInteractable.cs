using UnityEngine;

public interface IInteractable
{
    public void HighlightObject();

    public void UnHighlightObject();

    public void DisplayObjectName();

    public void RemoveObjectName();

    public void Pickup(GameObject pickUpDest);

    public void Drop();

    public void Equip(GameObject equipLocation);

    public void UnEquip();

    public void UnEquipAction();

    /// <summary>
    /// Uses the action of this object, defaults to throw.
    /// </summary>
    public void EquipAction(ref IInteractable equipedItem);
}
