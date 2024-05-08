using System.Collections;
using UnityEngine;

public class BossFollow : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;
    public float pauseDuration = 3f;
    public float moveDuration = 3f;

    private bool isMoving = true;

    void Start()
    {
        StartCoroutine(FollowPlayer());
    }

    private IEnumerator FollowPlayer()
    {
        while (true) // Loop indefinitely
        {
            if (player != null)
            {
                // Move towards the player continuously
                // Debug.Log("Following");
                Vector3 newPosition = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                transform.position = newPosition;
            }
            yield return null; // Wait until the next frame
        }
    }
}