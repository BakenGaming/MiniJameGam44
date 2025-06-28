using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using CodeMonkey.Utils;
using UnityEngine.EventSystems;

public class PlayerInputController_TopDown : MonoBehaviour, IInputHandler
{
    #region Events
    public static event Action<IGatherable> OnPlayerGatherAttempt;
    public static Action OnPauseGame;
    public static Action OnUnpauseGame;
    #endregion

    #region Variables
    private StatSystem _playerStats;
    private InputAction _move, _gather, _pause, _mousePos;
    private GameControls _controller;
    private Vector2 moveInput, lookDir;
    private Rigidbody2D playerRB;
    private Camera mainCam;
    private IGatherable currentGatherableTarget;

    #endregion

    #region Initialize
    public void Initialize()
    {
        _playerStats = GetComponent<IHandler>().GetStatSystem();
        _controller = new GameControls();
        playerRB = GetComponent<Rigidbody2D>();
        mainCam = Camera.main;

        _move = _controller.TopDownControls.MoveInput;
        _move.Enable();

        _mousePos = _controller.TopDownControls.MousePosition;
        _mousePos.Enable();

        _pause = _controller.TopDownControls.Pause;
        _pause.performed += HandlePauseInput;
        _pause.Enable();

        _gather = _controller.TopDownControls.Attack;
        _gather.performed += HandleGatherInput;
        _gather.Enable();
    }

    private void OnDisable()
    {
        _move.Disable();
        _gather.Disable();
        _pause.Disable();
        _mousePos.Disable();
    }
    #endregion

    #region Input Handling

    private void HandleGatherInput(InputAction.CallbackContext context)
    {
        if (currentGatherableTarget == null) { Debug.Log("Not Clicking on Object"); return; }
        if (currentGatherableTarget.GetAbleToGather()) OnPlayerGatherAttempt(currentGatherableTarget);
    }
    private void HandlePauseInput(InputAction.CallbackContext context)
    {
        if (!GameManager.i.GetIsPaused()) OnPauseGame?.Invoke();
        else OnUnpauseGame?.Invoke();
    }

    #endregion

    #region Loop
    private void Update()
    {
        if (!GameManager.i.GetIsPaused()) moveInput = _move.ReadValue<Vector2>();
        else playerRB.linearVelocity = Vector2.zero;
    }

    private void FixedUpdate()
    {
        if (GameManager.i.GetIsPaused())
        {
            playerRB.linearVelocity = Vector2.zero;
            return;
        }

        Vector3 mousePosition = _mousePos.ReadValue<Vector2>();
        Vector3 screenPoint = mainCam.WorldToScreenPoint(transform.localPosition);

        Flip(mousePosition.x, screenPoint.x);

        Vector2 moveSpeed = moveInput.normalized;
        playerRB.linearVelocity = new Vector2(moveSpeed.x * _playerStats.GetMoveSpeed(), moveSpeed.y * .5f * _playerStats.GetMoveSpeed());
    }

    #endregion

    #region Checks
    private void Flip(float mPosX, float sPtX)
    {
        if (mPosX < sPtX)
        {
            transform.Find("Sprite").GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            transform.Find("Sprite").GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    #endregion
    #region SetVariables
    public void SetGatherableTarget(IGatherable gatherable)
    {
        currentGatherableTarget = gatherable;
    }

    public void RemoveGatherableTarget()
    { 
        currentGatherableTarget = null;
    }
    #endregion
}
