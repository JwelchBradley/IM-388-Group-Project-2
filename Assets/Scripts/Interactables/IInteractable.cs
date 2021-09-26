/*****************************************************************************
// File Name :         IInteractable.cs
// Author :            Jacob Welch
// Creation Date :     28 August 2021
//
// Brief Description : An interface for interactable objects.
*****************************************************************************/
using UnityEngine;

public interface IInteractable
{
    /// <summary>
    /// Highlights this gameobject.
    /// </summary>
    public void HighlightObject();

    /// <summary>
    /// Stops highlighting this gameobject.
    /// </summary>
    public void UnHighlightObject();

    /// <summary>
    /// Displays the name of this object.
    /// </summary>
    public void DisplayObjectName(bool canHold, bool canEquipAction);

    /// <summary>
    /// Stops displaying the name of this object.
    /// </summary>
    public void RemoveObjectName();

    /// <summary>
    /// Picks up this object.
    /// </summary>
    /// <param name="pickUpDest">The location this object will be held at.</param>
    public void Pickup(GameObject pickUpDest);

    /// <summary>
    /// Drops this gameobject.
    /// </summary>
    public void Drop();

    /// <summary>
    /// Equips this object and moves it to the equip location.
    /// </summary>
    /// <param name="equipLocation">The location this object will be equipped to.</param>
    public void Equip(GameObject equipLocation);

    /// <summary>
    /// Unequips this game object and resets its values.
    /// </summary>
    public void UnEquip();

    /// <summary>
    /// The action for unequipping this object, defaults to a light throw.
    /// </summary>
    public void UnEquipAction();

    /// <summary>
    /// Uses the action of this object, defaults to throw.
    /// </summary>
    public void EquipAction(ref IInteractable equipedItem);

    /// <summary>
    /// Returns if this objected can be equipped.
    /// </summary>
    /// <returns></returns>
    public bool CanEquip();
}
