using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtingusher : InteractableObject
{
    ParticleSystem steam;

    private void Start()
    {
        steam = GetComponentInChildren<ParticleSystem>();
    }

    public override void EquipAction(ref IInteractable equipedItem)
    {
        steam.Play();
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            steam.Stop();
        }
    }
}
