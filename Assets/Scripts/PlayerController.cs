using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{

    private PlayerInputActions _playerInputActions;
    private InputAction _movement;

    void Awake()
    {
        _playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        // on enable we set up our movement property and enable it 
        _movement = _playerInputActions.Player.Movement;
        _movement.Enable();

        // register the jump action and trigger it on press
        // _playerInputActions.Player.Jump.performed += PlayerJump;
        // _playerInputActions.Player.Jump.Enable();

        // register the crouch action as a hold and release action
        // _playerInputActions.Player.Crouch.performed += PlayerCrouch;
        // _playerInputActions.Player.Crouch.canceled += PlayerCancelCrouch;
        // _playerInputActions.Player.Crouch.Enable();

    }

    private void OnDisable()
    {
        // we disable the controls if the object is disabled
        _movement.Disable();
        // _playerInputActions.Player.Jump.Disable();
        // _playerInputActions.Player.Crouch.Disable();
    }
}
