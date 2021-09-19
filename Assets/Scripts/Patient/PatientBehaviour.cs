using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientBehaviour : MonoBehaviour
{
    #region Timer
    [SerializeField]
    [Tooltip("How long before this patient dies")]
    private float survivalTime;

    [SerializeField]
    [Tooltip("How long before the first hint is given")]
    private float hintTime1;

    [SerializeField]
    [Tooltip("How long before the second hint is given")]
    private float hintTime2;

    [SerializeField]
    [Tooltip("How long before the second hint is given")]
    private float hintTime3;

    [SerializeField]
    [Tooltip("The visual timer for this patient")]
    private GameObject survivalClock;
    #endregion

    #region Hints
    [Header("Hints")]
    [SerializeField]
    [Tooltip("An array of the first hints this patient might give")]
    private string[] hint1;

    [SerializeField]
    [Tooltip("An array of the second hints this patient might give")]
    private string[] hint2;

    [SerializeField]
    [Tooltip("An array of the third hints this patient might give")]
    private string[] hint3;
    #endregion

    #region Solution
    [Tooltip("The number of problems on this patient")]
    private int numProblems;
    #endregion

    private void Awake()
    {
        StartCoroutine(DisplayHints());
    }

    private IEnumerator DisplayHints()
    {
        yield return new WaitForSeconds(hintTime1);

        yield return new WaitForSeconds(hintTime2);

        yield return new WaitForSeconds(hintTime3);
    }

    public void UpdateProblems()
    {
        numProblems--;

        if(numProblems == 0)
        {
            CurePatient();
        }
    }

    private void CurePatient()
    {

    }
}
