using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    private TextMeshProUGUI displayName;
    private string objectName = "Cube";
    private Rigidbody rb;
    private Outline outline;
    private float outlineWidth = 10;
    private float objectMoveSpeed = 12;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        outline = GetComponent<Outline>();
        displayName = GameObject.Find("Display Name").GetComponent<TextMeshProUGUI>();
    }

    public void DisplayObjectName()
    {
        displayName.text = objectName;
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
