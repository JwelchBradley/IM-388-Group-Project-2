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

    [Header("Pickup")]
    [SerializeField]
    [Tooltip("The width of the outline when it is hovered over")]
    [Range(5, 30)]
    private float outlineWidth = 10;

    [SerializeField]
    [Tooltip("How fast the object is pulled to the player")]
    [Range(5, 30)]
    private float objectMoveSpeed = 20;
    #endregion

    #region Functions
    #region Initialize
#if UNITY_EDITOR
    /// <summary>
    /// Initializes the components in the editor.
    /// </summary>
    private void Reset()
    {
        gameObject.layer = LayerMask.NameToLayer("Interactable");

        GetComponent<MeshCollider>().convex = true;
        Outline outline = GetComponent<Outline>();
        outline.OutlineWidth = 0;
        outline.OutlineColor = Color.yellow;
        outline.PreComputeOutline = true;
    }
#endif

    /// <summary>
    /// Initializes components needed by this object.
    /// </summary>
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        outline = GetComponent<Outline>();
        mesh = GetComponent<MeshCollider>();

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
        outline.OutlineWidth = outlineWidth;
    }

    /// <summary>
    /// Stops the highlight on the object.
    /// </summary>
    public void UnHighlightObject()
    {
        outline.OutlineWidth = 0;
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
                else
                {
                    canGrab = true;
                }
            }

            if(canGrab)
            {
                Vector3 dir = pickUpDest.transform.position - rb.position;
                float currentMoveSpeed = Mathf.Lerp(0, objectMoveSpeed, Vector3.Distance(rb.position, pickUpDest.transform.position));
                dir = dir.normalized * currentMoveSpeed;
                rb.velocity = dir;
            }

            yield return new WaitForEndOfFrame();
        }
    }

    public void Drop()
    {
        rb.useGravity = true;
        transform.parent = null;
        StopAllCoroutines();
    }
    #endregion

    #region Equip
    public void Equip()
    {
        throw new System.NotImplementedException();
    }
    #endregion
    #endregion
    #endregion
}
