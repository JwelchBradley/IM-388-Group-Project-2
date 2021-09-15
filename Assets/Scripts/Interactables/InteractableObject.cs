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

    [Header("Pickup")]
    [SerializeField]
    [Tooltip("The width of the outline when it is hovered over")]
    [Range(5, 30)]
    private float outlineWidth = 10;

    [SerializeField]
    [Tooltip("How fast the object is pulled to the player")]
    [Range(5, 30)]
    private float objectMoveSpeed = 12;


#if UNITY_EDITOR
    /// <summary>
    /// Initializes the components in the editor.
    /// </summary>
    private void Reset()
    {
        GetComponent<MeshCollider>().convex = true;
        Outline outline = GetComponent<Outline>();
        outline.OutlineWidth = 0;
        outline.OutlineColor = Color.yellow;
    }
#endif

    /// <summary>
    /// Initializes components needed by this object.
    /// </summary>
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        outline = GetComponent<Outline>();

        displayName = GameObject.Find("Display Name").GetComponent<TextMeshProUGUI>();
    }

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

    /// <summary>
    /// Highlights the object.
    /// </summary>
    public void HighlightObject()
    {
        outline.OutlineWidth = outlineWidth;
    }

    public void UnHighlightObject()
    {
        outline.OutlineWidth = 0;
    }

    public void Pickup(GameObject pickUpDest)
    {
        StartCoroutine(PickupHelper(pickUpDest));
        /*
        rb.useGravity = false;
        transform.position = pickUpDest.transform.position;
        transform.parent = pickUpDest.transform;*/
    }

    private IEnumerator PickupHelper(GameObject pickUpDest)
    {
        rb.useGravity = false;
        while (true)
        {
            Vector3 dir = pickUpDest.transform.position - rb.position;
            float currentMoveSpeed = Mathf.Lerp(0, objectMoveSpeed, Vector3.Distance(rb.position, pickUpDest.transform.position));
            dir = dir.normalized * currentMoveSpeed;
            rb.velocity = dir;
            //rb.position = Vector3.MoveTowards(rb.position, pickUpDest.transform.position, objectMoveSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    public void Drop()
    {
        rb.useGravity = true;
        transform.parent = null;
        StopAllCoroutines();
    }

    public void Equip()
    {
        throw new System.NotImplementedException();
    }
}
