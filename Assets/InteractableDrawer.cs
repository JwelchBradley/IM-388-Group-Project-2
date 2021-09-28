using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    private TextMeshProUGUI text;

    private PlayerPickup pp;

    private string action = "open";

    private float time = 0;

    private void Awake()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();
        startPos = rb.position;
        outline = GetComponent<Outline>();
        pp = GameObject.Find("Player").GetComponent<PlayerPickup>();
        text = GameObject.Find("Display Name").GetComponent<TextMeshProUGUI>();
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

        bool playerNotHolding = pp.HeldItem == null && pp.EquipedItem == null;

        if (closeness && playerNotHolding)
        {
            text.text = "<sprite index=0> to " + action;
        }

        if (closeness && !outline.isActiveAndEnabled)
        {
            outline.enabled = true;
        }
        else if(!closeness)
        {
            if(playerNotHolding)
            text.text = "";

            outline.enabled = false;
        }
    }

    private void OnMouseDown()
    {
        if (PlayerCloseEnough())
        {
            if (transform.position != startPos)
            {
                action = "open";
                StopAllCoroutines();
                StartCoroutine(MoveDrawerIn());
            }
            else
            {
                action = "close";
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
        bool playerNotHolding = pp.HeldItem == null && pp.EquipedItem == null;

        if(playerNotHolding)
        text.text = "";

        outline.enabled = false;
    }
}
