using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float speed = 5f;

    public enum PlayerMovementType { tf, physics };
    [SerializeField] PlayerMovementType movementType = PlayerMovementType.tf;
    
    [Header("Physics")]


    [Header("Flavor")]
    [SerializeField] string playerName = "Leuk";
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
    }

    public void MovePlayerTransform(Vector3 direction){
        transform.position += direction * Time.deltaTime * speed;
    }

    public void MovePlayerRigidBody(Vector3 direction){
        Vector3 currentVelocity = new Vector3(0, rigidBody.velocity.y, 0);
        rigidBody.velocity = (currentVelocity) + (direction * speed);
    }
}
