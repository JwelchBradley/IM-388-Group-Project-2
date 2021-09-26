using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    #region Variables
    #region Interactaction Attributes
    [Header("Pickup")]
    [SerializeField]
    [Tooltip("The speed the player moves while standing")]
    [Range(0, 10)]
    private float pickupDist = 2f;

    [SerializeField]
    [Tooltip("The position that the object will be picked up to")]
    private GameObject pickUpDest;

    [SerializeField]
    [Tooltip("The position that the object will be equiped to")]
    private GameObject equipLocation;

    [SerializeField]
    [Tooltip("The mask of interactable objects")]
    private LayerMask interactableMask;
    #endregion

    #region Item References
    /// <summary>
    /// The interactable script of a held item.
    /// </summary>
    private IInteractable heldItem = null;

    public IInteractable HeldItem
    {
        get { return heldItem; }
        set
        {
            heldItem = value;
        }
    }

    /// <summary>
    /// The interactable script of a hovered item.
    /// </summary>
    private IInteractable hoveredItem = null;

    public IInteractable HoverItem
    {
        get { return hoveredItem; }
        set
        {
            hoveredItem = value;
        }
    }

    /// <summary>
    /// The interactalbe script of the equiped item.
    /// </summary>
    private IInteractable equipedItem = null;

    public IInteractable EquipedItem
    {
        get { return equipedItem; }
        set
        {
            equipedItem = value;
        }
    }

    private bool canUnEquip = false;
    #endregion

    /// <summary>
    /// The main camera in this scene.
    /// </summary>
    private Camera main;
    #endregion

    #region Functions
    #region Initiliaze
    /// <summary>
    /// Gets the main camera in the scene.
    /// </summary>
    private void Awake()
    {
        main = Camera.main;     
    }
    #endregion

    /// <summary>
    /// Checks if the player wants to drop a held item.
    /// </summary>
    private void Update()
    {
        if (heldItem != null && Input.GetKeyUp(KeyCode.Mouse0))
        {
            heldItem.Drop();
            heldItem = null;
        }

        if(equipedItem != null)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                equipedItem.EquipAction(ref equipedItem);
                canUnEquip = false;
            }
            if (Input.GetKeyDown(KeyCode.Mouse1) && canUnEquip)
            {
                ResetEquippedItem();
            }
        }
        else if(heldItem != null)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                EquipItem(heldItem);
            }
        }
    }

    #region Hover
    /// <summary>
    /// Checks if the object the player is looking at is an interactable object.
    /// </summary>
    public void CheckObject()
    {
        if(heldItem == null)
        {
            // Cast ray at center of veiwport
            Ray ray = main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            RaycastHit hit;

            // Casts out to see if there is an interactable object
            if (Physics.Raycast(ray, out hit, pickupDist, interactableMask))
            {
                IInteractable interactable = hit.transform.gameObject.GetComponent<IInteractable>();

                if(interactable != null)
                HandleInteractableHover(interactable);
            }
            else
            {
                ResetHoverItem();
            }
        }
    }

    /// <summary>
    /// Sets what the current hover object is.
    /// </summary>
    /// <param name="interactable">The currently hovered interactable object.</param>
    private void SetHoverObject(IInteractable interactable)
    {
        ResetHoverItem();

        // Makes object the current Hover Object
        interactable.HighlightObject();

        if(equipedItem == null)
        {
            interactable.DisplayObjectName(true, false);
        }

        hoveredItem = interactable;
    }

    /// <summary>
    /// Resets the objects to its state prior to being hovered over.
    /// </summary>
    private void ResetHoverItem()
    {
        if(hoveredItem != null && heldItem == null)
        {
            if(equipedItem == null)
            {
                hoveredItem.RemoveObjectName();
            }

            hoveredItem.UnHighlightObject();
            hoveredItem = null;
        }
    }
    #endregion

    #region Equip
    /// <summary>
    /// Handles the interactions when an interactable object is being hovered over.
    /// </summary>
    /// <param name="interactable"></param>
    private void HandleInteractableHover(IInteractable interactable)
    {
        // Switchs the hovered item to the new hovered item
        if(hoveredItem != interactable && equipedItem != interactable)
        {
            SetHoverObject(interactable);
        }

        // Picks up the item
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(equipedItem == null)
            {
                interactable.Pickup(pickUpDest);
                heldItem = interactable;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            EquipItem(interactable);
        }
    }

    /// <summary>
    /// Sets what the current equiped item is.
    /// </summary>
    /// <param name="interactable">The currently hovered interactable object.</param>
    private void EquipItem(IInteractable interactable)
    {
        interactable.DisplayObjectName(false, true);

        if (equipedItem != interactable && interactable.CanEquip())
        {
            if(heldItem != null)
            {
                heldItem.UnHighlightObject();
                heldItem.Drop();
                heldItem = null;
            }

            ResetEquippedItem();
            Invoke("AllowUnequip", 0.01f);
            interactable.Equip(equipLocation);
            equipedItem = interactable;
        }
    }

    private void AllowUnequip()
    {
        canUnEquip = true;
    }

    /// <summary>
    /// Resets any currently equipped item.
    /// </summary>
    private void ResetEquippedItem()
    {
        if(equipedItem != null)
        {
            canUnEquip = false;
            equipedItem.UnEquip();
            equipedItem.UnEquipAction();
            equipedItem = null;
        }
    }
    #endregion
    #endregion
}
