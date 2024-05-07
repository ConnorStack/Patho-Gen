using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNAProcessingZone : MonoBehaviour
{
    public float processingRate = 10f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Debug.Log("Enter the zone");
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                // player.StartProcessingTokens(processingRate);
                player.EnterProcessingZone(processingRate);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Debug.Log("Exit the zone");
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                // player.StopProcessingTokens();
                player.ExitProcessingZone();
            }
        }
    }
}
