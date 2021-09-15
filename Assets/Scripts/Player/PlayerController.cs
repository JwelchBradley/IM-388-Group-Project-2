using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PauseMenuBehavior pauseMenu;
    private PlayerMovement pm;
    private PlayerPickup pp;

    /// <summary>
    /// Initiliazes the player.
    /// </summary>
    private void Awake()
    {
        pauseMenu = GameObject.Find("Pause Menu Templates Canvas").GetComponent<PauseMenuBehavior>();
        pm = GetComponent<PlayerMovement>();
        pp = GetComponent<PlayerPickup>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    #region Inputs
    private void Update()
    {
        pp.CheckObject();
    }

    /// <summary>
    /// Pauses the game if the escape key is pressed.
    /// </summary>
    private void OnPauseGame()
    {
        pauseMenu.PauseGame();
    }

    /// <summary>
    /// Makes the player jump.
    /// </summary>
    public void OnJump()
    {
        pm.Jump();
    }

    /// <summary>
    /// Calls for the player to be moved.
    /// </summary>
    /// <param name="input">A vector 2 input direction.</param>
    public void OnMovement(InputValue input)
    {
        pm.MovePlayer(input.Get<Vector2>());

    }

    public void OnCrouch()
    {
        pm.Crouch();
    }
    #endregion
}
