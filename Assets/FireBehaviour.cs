using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBehaviour : MonoBehaviour
{
    ParticleSystem fire;
    ParticleSystem smoke;

    [SerializeField]
    PatientBehaviour patient;

    bool hasBeenPutOut = false;

    private void Awake()
    {
        fire = GetComponent<ParticleSystem>();
        smoke = GetComponentInChildren<ParticleSystem>();
    }

    public void StopParticles()
    {
        if (!hasBeenPutOut)
        {
            Debug.Log(true);
            patient.UpdateProblems();
            fire.Stop();
            smoke.Stop();
            hasBeenPutOut = true;
        }
    }
}
