using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float speed = 5f;
    [SerializeField] float maxVerticalSpeed = 10;

    public enum PlayerMovementType { tf, physics };
    [SerializeField] PlayerMovementType movementType = PlayerMovementType.tf;
    
    [Header("Physics")]


    [Header("Flavor")]
    [SerializeField] string playerName = "Leuk";
    [SerializeField] private GameObject body;
    [SerializeField] List<AnimationStateChanger> animationStateChangers;
    Rigidbody2D rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void MovePlayer(Vector3 direction){
        if(movementType == PlayerMovementType.tf){
            MovePlayerTransform(direction);
        }else if (movementType == PlayerMovementType.physics){
            MovePlayerRigidBody(direction);
        }
        //set animation
        if(direction != Vector3.zero){
            foreach(AnimationStateChanger asc in animationStateChangers){
                asc.ChangeAnimationState("LeukWalk1_Clip");
            }
        }else{
            foreach(AnimationStateChanger asc in animationStateChangers){
                asc.ChangeAnimationState("LeukIdle_Clip");
            }
        }
    }

    public void MovePlayerTransform(Vector3 direction){
        transform.position += direction * Time.deltaTime * speed;
        if (direction.x < 0){
            body.transform.localScale = new Vector3(-1, 1, 1); // Flip left
        } else if (direction.x > 0) {
            body.transform.localScale = new Vector3(1, 1, 1); // Flip right
        }
    }

    public void MovePlayerRigidBody2(Vector3 direction){
        Vector3 currentVelocity = new Vector3(0, rigidBody.velocity.y, 0);
        rigidBody.velocity = (currentVelocity) + (direction * speed);
        if(rigidBody.velocity.x < 0){
            body.transform.localScale = new Vector3(-1, 1, 1);
            Debug.Log("Flip!");
        }
    }

    public void MovePlayerRigidBody(Vector3 direction){
    // Calculate the desired velocity based on input direction
    Vector3 desiredVelocity = new Vector3(direction.x * speed, direction.y * speed, 0);

    // Optionally, you can apply a limit to the vertical movement speed to prevent extreme behavior
    desiredVelocity.y = Mathf.Clamp(desiredVelocity.y, -maxVerticalSpeed, maxVerticalSpeed);

    // Apply the desired velocity to the rigidbody
    rigidBody.velocity = desiredVelocity;

    // Flip the sprite based on horizontal movement
    if(direction.x < 0){
        body.transform.localScale = new Vector3(-1, 1, 1);
    } else if (direction.x > 0) {
        body.transform.localScale = new Vector3(1, 1, 1);
    }
}
}
