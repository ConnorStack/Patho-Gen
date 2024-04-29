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
        while (true)
        {
            if (isMoving)
            {

                if (player != null)
                {
                    Vector3 newPosition = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                    transform.position = newPosition;
                }


                yield return new WaitForSeconds(moveDuration);
                isMoving = false;
            }
            else
            {

                yield return new WaitForSeconds(pauseDuration);
                isMoving = true;
            }
        }
    }
}