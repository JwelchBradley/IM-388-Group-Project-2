/*****************************************************************************
// File Name :         InteractableDoor.cs
// Author :            Jacob Welch
// Creation Date :     28 August 2021
//
// Brief Description : An interactable object that has a hingejoint andcan't be
                       equipped.
*****************************************************************************/
using UnityEngine;

[RequireComponent(typeof(HingeJoint))]
public class InteractableDoor : InteractableObject
{
#if UNITY_EDITOR
    /// <summary>
    /// Initializes the components in the editor.
    /// </summary>
    private void Reset()
    {
        // Sets the objects layer to interactable
        gameObject.layer = LayerMask.NameToLayer("Interactable");

        // Initiliazes the meshcollider to be convex
        GetComponent<MeshCollider>().convex = true;

        canEquip = false;

        HingeJoint hj = GetComponent<HingeJoint>();
        hj.anchor = new Vector3(-0.02f, 0.01f, 0.02f);
        hj.axis = new Vector3(0, 0, 90);
        hj.useLimits = true;
        JointLimits limits = hj.limits;
        limits.max = 90;
        hj.limits = limits;

        // Initializes the outline component
        Outline outline = GetComponent<Outline>();
        outline.OutlineWidth = 10;
        outline.OutlineColor = Color.yellow;
        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.PreComputeOutline = true;
        outline.enabled = false;
    }
#endif
}
