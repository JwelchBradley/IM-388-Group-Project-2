using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDrawer : MonoBehaviour
{
    Outline outline;

    private Vector3 startPos;

    private Vector3 openPos;

    [SerializeField]
    private float amountForward = 2.4f;

    private void Awake()
    {
        startPos = transform.position;
        outline = GetComponent<Outline>();
        outline.enabled = false;
        openPos = startPos + transform.up * amountForward;
    }

    private void OnMouseEnter()
    {
        outline.enabled = true;
    }

    private void OnMouseDown()
    {
        if(transform.position != startPos)
        {
            transform.position = startPos;
        }
        else
        {
            transform.position = openPos;
        }
    }

    private void OnMouseExit()
    {
        outline.enabled = false;
    }
}
