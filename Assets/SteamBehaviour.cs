using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamBehaviour : MonoBehaviour
{
    ParticleSystem steam;

    [SerializeField]
    LayerMask mask;

    private void Awake()
    {
        steam = GetComponent<ParticleSystem>();
    }

    private void FixedUpdate()
    {
        if (steam.isEmitting)
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.forward, out hit, 4f, mask, QueryTriggerInteraction.Collide))
            {
                hit.transform.gameObject.GetComponent<FireBehaviour>().StopParticles();
                hit.transform.gameObject.SetActive(false);
            }
        }
    }

    public void StartSteam()
    {
        steam.Play();
    }
    public void StopSteam()
    {
        steam.Stop();
    }
}
