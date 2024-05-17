using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 playerMovementInput;

    [SerializeField] private Rigidbody playerBody;
    [SerializeField] public float walkSpeed;


    private void Update()
    {
        playerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 moveVector = transform.TransformDirection(playerMovementInput)* walkSpeed;
        playerBody.velocity = new Vector3(moveVector.x,playerBody.velocity.y, moveVector.z);

    }
}
