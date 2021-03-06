using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatientBehaviour : MonoBehaviour
{
    #region Timer
    [SerializeField]
    [Tooltip("How long before this patient dies in seconds")]
    private float survivalTime = 240;

    /// <summary>
    /// The amount of time currently left before the patient dies.
    /// </summary>
    private float timeLeft = 0;

    [SerializeField]
    [Tooltip("How long before the first hint is given")]
    private float hintTime1 = 60;

    [SerializeField]
    [Tooltip("How long before the second hint is given")]
    private float hintTime2 = 60;

    [SerializeField]
    [Tooltip("How long before the second hint is given")]
    private float hintTime3 = 60;

    [SerializeField]
    [Tooltip("The visual timer for this patient")]
    private Image survivalClock;
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
    [SerializeField]
    [Tooltip("The number of problems on this patient")]
    private int numProblems;
    #endregion

    [SerializeField]
    [Tooltip("The win screen for the game")]
    private GameObject winScreen;

    [SerializeField]
    [Tooltip("The lose screen for the game")]
    private GameObject loseScreen;

    private void Awake()
    {
        timeLeft = survivalTime;
        GetComponentInChildren<MenuBehavior>().crossfadeAnim = GameObject.Find("Pause Menu Templates Canvas").GetComponent<PauseMenuBehavior>().crossfadeAnim;
        StartCoroutine(DisplayHints());
    }

    private IEnumerator DisplayHints()
    {
        yield return new WaitForSeconds(hintTime1);

        yield return new WaitForSeconds(hintTime2);

        yield return new WaitForSeconds(hintTime3);
    }

    private void FixedUpdate()
    {
        timeLeft -= Time.fixedDeltaTime;
        survivalClock.fillAmount = timeLeft / survivalTime;

        if(timeLeft < 0)
        {
            PatientDeath();
        }
    }

    public void UpdateProblems()
    {
        numProblems--;

        if(numProblems == 0)
        {
            CurePatient();
        }
    }

    private void PatientDeath()
    {
        DisplayWinLoseScreen(loseScreen);
    }

    private void CurePatient()
    {
        DisplayWinLoseScreen(winScreen);
    }

    private void DisplayWinLoseScreen(GameObject screen)
    {
        GameObject pauseMenu = GameObject.Find("Pause Menu Templates Canvas");
        pauseMenu.GetComponent<PauseMenuBehavior>().canPause = false;
        pauseMenu.SetActive(false);

        GameObject.Find("Walk vcam").SetActive(false);
        GameObject.Find("Crouch vcam").SetActive(false);

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        Time.timeScale = 0;

        screen.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Chair"))
        {
            //UpdateProblems();
        }
    }

}
