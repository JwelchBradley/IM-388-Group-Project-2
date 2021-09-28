using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasCanBehaviour : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The explosion spawned when the gas can collides with fire")]
    private GameObject explosion;

    private PlayerPickup player;

    [SerializeField]
    private AudioClip explosionSound;

    private IInteractable interactable;

    private void Awake()
    {
        interactable = GetComponent<IInteractable>();
        player = GameObject.Find("Player").GetComponent<PlayerPickup>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Fire")))
        {
            bool isHolding = player.HeldItem == interactable || player.EquipedItem == interactable;

            FireBehaviour fb = other.gameObject.GetComponent<FireBehaviour>();

            if (isHolding)
            {
                player.HeldItem = null;
                player.HoverItem = null;
                player.EquipedItem = null;
                interactable.RemoveObjectName();
            }

            fb.patient.timeLeft -= 30;

            GameObject tempexplosion = Instantiate(explosion, transform.position, Quaternion.identity);

            Destroy(tempexplosion, 4);
            Destroy(gameObject);
        }
    }
}
