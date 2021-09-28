using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDrawer : MonoBehaviour
{
    Outline outline;

    private Vector3 startPos;

    private Vector3 openPos;

    private Rigidbody rb;

    [SerializeField]
    private float amountForward = 2.4f;

    private GameObject player;

    private Coroutine currentMove;

    private float time = 0;

    private void Awake()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();
        startPos = rb.position;
        outline = GetComponent<Outline>();
        outline.enabled = false;
        openPos = startPos + transform.up * amountForward;
    }

    private bool PlayerCloseEnough()
    {
        return Vector3.Distance(player.transform.position, transform.position) <= 6;
    }

    private void OnMouseOver()
    {
        bool closeness = PlayerCloseEnough();

        if (closeness && !outline.isActiveAndEnabled)
        {
            outline.enabled = true;
        }
        else if(!closeness)
        {
            outline.enabled = false;
        }
    }

    private void OnMouseDown()
    {
        if (PlayerCloseEnough())
        {
            if (transform.position != startPos)
            {
                StopAllCoroutines();
                StartCoroutine(MoveDrawerIn());
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(MoveDrawerOut());
            }
        }
    }

    private IEnumerator MoveDrawerOut()
    {
        Vector3 drawerPos = rb.position;

        while (transform.position != openPos)
        {
            time += Time.fixedDeltaTime*2;
            if(time >= 2)
            {
                time = 2;
            }
            rb.position = Vector3.MoveTowards(drawerPos, openPos, time);
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator MoveDrawerIn()
    {
        Vector3 drawerPos = rb.position;

        while (transform.position != startPos)
        {
            time -= Time.fixedDeltaTime*2;
            if (time < 0)
            {
                time = 0;
            }
            rb.position = Vector3.MoveTowards(startPos, drawerPos, time);
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnMouseExit()
    {
        outline.enabled = false;
    }
}
