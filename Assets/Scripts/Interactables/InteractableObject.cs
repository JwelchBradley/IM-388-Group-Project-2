/*****************************************************************************
// File Name :         InteractalbeObject.cs
// Author :            Jacob Welch
// Creation Date :     28 August 2021
//
// Brief Description : Allows the player to 
*****************************************************************************/
using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Outline))]
[RequireComponent(typeof(MeshCollider))]
public class InteractableObject : MonoBehaviour, IInteractable
{
    #region Variables
    #region Components
    /// <summary>
    /// The TextMeshProUGUI component in the scene the renders the objects name.
    /// </summary>
    private TextMeshProUGUI displayName;

    /// <summary>
    /// The rigidbody of this object.
    /// </summary>
    private Rigidbody rb;

    /// <summary>
    /// The outline component of this object.
    /// </summary>
    private Outline outline;

    /// <summary>
    /// The MeshCollider of this object.
    /// </summary>
    private MeshCollider mesh;
    #endregion

    #region Pickup
    [Header("Pickup")]
    [SerializeField]
    [Tooltip("How fast the object is pulled to the player")]
    [Range(5, 30)]
    private float objectMoveSpeed = 12;
    #endregion

    #region Equip
    [Header("Equip")]
    [SerializeField]
    [Tooltip("The offset from the default equip position")]
    private Vector3 equipOffset = Vector3.zero;

    [SerializeField]
    [Tooltip("The roation of the object when it is equiped")]
    private Vector3 equipRotation = Vector3.zero;

    [SerializeField]
    [Tooltip("The force applied to the object when it is unequiped")]
    protected float unequipThrowForce = 800;

    [SerializeField]
    [Tooltip("The force of throwing the object")]
    private float throwForce = 1200;
    #endregion

    /// <summary>
    /// The main camera of this scene.
    /// </summary>
    private Camera mainCamera;
    #endregion

    #region Functions
    #region Initialize
#if UNITY_EDITOR
    /// <summary>
    /// Initializes the components in the editor.
    /// </summary>
    private void Reset()
    {
        // Sets the objects layer to interactable
        gameObject.layer = LayerMask.NameToLayer("Interactable");

        // Initiliazes the meshcollider to be convex
        GetComponent<MeshCollider>().convex = true;

        // Initializes the outline component
        Outline outline = GetComponent<Outline>();
        outline.OutlineWidth = 10;
        outline.OutlineColor = Color.yellow;
        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.PreComputeOutline = true;
        outline.enabled = false;
    }
#endif

    /// <summary>
    /// Initializes components needed by this object.
    /// </summary>
    private void Awake()
    {
        // Gets this objects components
        rb = GetComponent<Rigidbody>();
        outline = GetComponent<Outline>();
        mesh = GetComponent<MeshCollider>();

        // Gets other objects components
        mainCamera = Camera.main;
        displayName = GameObject.Find("Display Name").GetComponent<TextMeshProUGUI>();
    }
    #endregion

    #region Interactions
    #region Object Name Display
    /// <summary>
    /// Displays the objects name.
    /// </summary>
    public void DisplayObjectName()
    {
        displayName.text = gameObject.name;
    }

    /// <summary>
    /// Removes the objects name.
    /// </summary>
    public void RemoveObjectName()
    {
        displayName.text = "";
    }
    #endregion

    #region Highlight
    /// <summary>
    /// Highlights the object.
    /// </summary>
    public void HighlightObject()
    {
        outline.enabled = true;
    }

    /// <summary>
    /// Stops the highlight on the object.
    /// </summary>
    public void UnHighlightObject()
    {
        outline.enabled = false;
    }
    #endregion

    #region Pickup
    public void Pickup(GameObject pickUpDest)
    {
        StartCoroutine(PickupHelper(pickUpDest));
    }

    private IEnumerator PickupHelper(GameObject pickUpDest)
    {
        rb.useGravity = false;

        bool canGrab = true;

        while (true)
        {
            foreach(Collider col in PlayerMovement.playerIsOn)
            {
                if (mesh.Equals(col))
                {
                    canGrab = false;
                    break;
                }
            }

            if(PlayerMovement.playerIsOn.Length == 0)
            {
                canGrab = true;
            }

            if(canGrab)
            {
                Vector3 dir = pickUpDest.transform.position - rb.position;
                float currentMoveSpeed = Mathf.Lerp(0, objectMoveSpeed, Vector3.Distance(rb.position, pickUpDest.transform.position));
                dir = dir.normalized * currentMoveSpeed;
                rb.velocity = dir;
            }
            else
            {
                //rb.velocity = Vector3.zero;
            }

            yield return new WaitForEndOfFrame();
        }
    }

    public void Drop()
    {
        rb.useGravity = true;
        StopAllCoroutines();
    }
    #endregion

    #region Equip
    /// <summary>
    /// Equips an item to the equiplocation with an offeset.
    /// </summary>
    /// <param name="equipLocation"></param>
    public void Equip(GameObject equipLocation)
    {
        // Sets the objects collision
        rb.isKinematic = true;
        gameObject.layer = LayerMask.NameToLayer("Equipped");

        // Sets the objects position and rotation
        transform.rotation = Quaternion.Euler(equipRotation);
        transform.position = equipLocation.transform.position + equipOffset;
        transform.parent = equipLocation.transform;
    }

    /// <summary>
    /// Unequips the object from the player.
    /// </summary>
    public void UnEquip()
    {
        StartCoroutine(UnEquipHelper());
        rb.isKinematic = false;
        transform.parent = null;
    }

    private IEnumerator UnEquipHelper()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }

    /// <summary>
    /// Throws the object if it is equipped.
    /// </summary>
    public void Throw(float throwForce)
    {
        rb.AddForce(mainCamera.transform.forward * throwForce);
    }

    /// <summary>
    /// Uses the action of this object, defaults to throw.
    /// </summary>
    public void EquipAction(ref IInteractable equipedItem)
    {
        UnEquip();
        Throw(throwForce);
        equipedItem = null;
    }

    /// <summary>
    /// Throws the object forward a little bit.
    /// </summary>
    public void UnEquipAction()
    {
        Throw(unequipThrowForce);
    }
    #endregion
    #endregion
    #endregion
}
