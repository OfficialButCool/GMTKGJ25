using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerRespawn respawn = other.GetComponent<PlayerRespawn>();
        if (respawn != null)
        {
            Debug.Log("Entered kill zone! Respawning...");
            respawn.Respawn();
        }
    }
}

