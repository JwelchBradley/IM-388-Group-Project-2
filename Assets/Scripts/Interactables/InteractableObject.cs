using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Outline))]
[RequireComponent(typeof(MeshCollider))]
public class InteractableObject : MonoBehaviour, IInteractable
{
    private TextMeshProUGUI displayName;

    private Rigidbody rb;
    private Outline outline;

    [SerializeField]
    private float outlineWidth = 10;
    private float objectMoveSpeed = 12;


#if UNITY_EDITOR
    private void Reset()
    {
        GetComponent<MeshCollider>().convex = true;
        Outline outline = GetComponent<Outline>();
        outline.OutlineWidth = 0;
        outline.OutlineColor = Color.yellow;
    }
#endif

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        outline = GetComponent<Outline>();

        displayName = GameObject.Find("Display Name").GetComponent<TextMeshProUGUI>();
    }

    public void DisplayObjectName()
    {
        displayName.text = gameObject.name;
    }

    public void RemoveObjectName()
    {
        displayName.text = "";
    }

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
