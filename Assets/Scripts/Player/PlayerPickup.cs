using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    #region Variables
    [Header("Pickup")]
    [SerializeField]
    [Tooltip("The speed the player moves while standing")]
    [Range(0, 10)]
    private float pickupDist = 2f;

    [SerializeField]
    [Tooltip("The position that the object will be picked up to")]
    private GameObject pickUpDest;

    /// <summary>
    /// The interactable script of a held item.
    /// </summary>
    private IInteractable heldItem = null;

    /// <summary>
    /// The interactable script of a hovered item.
    /// </summary>
    private IInteractable hoveredItem = null;

    /// <summary>
    /// The main camera in this scene.
    /// </summary>
    private Camera main;
    #endregion

    #region Functions
    /// <summary>
    /// Gets the main camera in the scene.
    /// </summary>
    private void Awake()
    {
        main = Camera.main;     
    }

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
    }

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

            if (Physics.Raycast(ray, out hit, pickupDist))
            {
                IInteractable interactable = hit.transform.gameObject.GetComponent<IInteractable>();

                if (interactable != null)
                {
                    HandleInteractableHover(interactable);
                }
                else
                {
                    ResetHoverItem();
                }
            }
            else
            {
                ResetHoverItem();
            }
        }
    }

    /// <summary>
    /// Resets the objects to its state prior to being hovered over.
    /// </summary>
    private void ResetHoverItem()
    {
        if(hoveredItem != null && heldItem == null)
        {
            hoveredItem.RemoveObjectName();
            hoveredItem.UnHighlightObject();
            hoveredItem = null;
        }
    }

    /// <summary>
    /// Handles the interactions when an interactable object is being hovered over.
    /// </summary>
    /// <param name="interactable"></param>
    private void HandleInteractableHover(IInteractable interactable)
    {
        if(hoveredItem != interactable)
        {
            ResetHoverItem();

            interactable.HighlightObject();
            interactable.DisplayObjectName();

            hoveredItem = interactable;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            interactable.Pickup(pickUpDest);
            heldItem = interactable;
        }
    }
    #endregion
}
