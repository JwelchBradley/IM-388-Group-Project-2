using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBehaviour : MonoBehaviour
{
    ParticleSystem fire;
    ParticleSystem smoke;

    [SerializeField]
    PatientBehaviour patient;

    private void Awake()
    {
        fire = GetComponent<ParticleSystem>();
        smoke = GetComponentInChildren<ParticleSystem>();
    }

    public void StopParticles()
    {
        patient.UpdateProblems();
        fire.Stop();
        smoke.Stop();
    }
}
