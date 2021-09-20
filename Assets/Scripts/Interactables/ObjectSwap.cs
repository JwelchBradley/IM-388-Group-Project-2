/*****************************************************************************
// File Name :         ObjectSwap.cs
// Author :            Connor Dunn
// Creation Date :     20 September 2021
//
// Brief Description : Changes the object appearance when it has been thrown.
*****************************************************************************/
using UnityEngine;

public class ObjectSwap : MonoBehaviour
{
    [Tooltip("Secondary game object that the prefab turns into.")]
    public GameObject NewObject;

    [Tooltip("Speed at which the object needs to be hit to transform.")]
    public float velocityThreshold;
    Vector3 velocity;
    Transform orientation;
    bool hasBeenHit = false;

    // When the object has been hit by something.
    void OnCollisionEnter(Collision collision)
    {
        velocity = GetComponent<Rigidbody>().velocity;
        orientation = gameObject.GetComponent<Transform>();

        if(velocity.magnitude >= velocityThreshold && !hasBeenHit)
        {
            this.gameObject.GetComponent<Renderer>().enabled = false;
            Instantiate(NewObject, orientation.position, orientation.rotation);
            hasBeenHit = true;
        }
    }
}
