/*****************************************************************************
// File Name :         CollisionSound.cs
// Author :            Jacob Welch
// Creation Date :     28 August 2021
//
// Brief Description : Gives this object a sound upon collision.
*****************************************************************************/
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class CollisionSound : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The amount of velocity needed to play any sound from the collision")]
    [Range(0, 20)]
    float requiredHitVelocity = 4;

    /// <summary>
    /// The audiosource on this gameobject.
    /// </summary>
    private AudioSource aud;

#if UNITY_EDITOR
    private void Reset()
    {
        AudioMixer mixer = Resources.Load("AudioMaster") as AudioMixer;
        aud = GetComponent<AudioSource>();
        aud.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
        aud.playOnAwake = false;
        aud.spatialBlend = 1;
        aud.rolloffMode = AudioRolloffMode.Custom;
        aud.minDistance = 50;
        aud.maxDistance = 200;
    }
#endif

    /// <summary>
    /// Gets the audiosource component.
    /// </summary>
    private void Awake()
    {
        aud = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Plays the audiosource if there is enough relative velocity in the collision.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.relativeVelocity.magnitude > requiredHitVelocity)
        {
            float soundVol = 0.075f * collision.relativeVelocity.magnitude;
            soundVol = Mathf.Clamp(soundVol, 0, 1);
            aud.PlayOneShot(aud.clip, soundVol);
        }
    }
}
