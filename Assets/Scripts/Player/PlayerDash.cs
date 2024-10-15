using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : MonoBehaviour
{
   //[SerializeField] private float pushForce = 5f;
    private Rigidbody2D rb;


    PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        playerController.inputActions.PlayerAction.Dash.started += DashSetting;
    }

    private void DashSetting(InputAction.CallbackContext context)
    {
        
    }


}
