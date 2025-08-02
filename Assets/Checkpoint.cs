using UnityEngine;

public class CheckpointWithRoomLoader : MonoBehaviour
{
    public GameObject[] roomsToEnable;
    public GameObject[] roomsToDisable;

    private AudioSource audioSource;
    private bool hasTriggered = false; // Prevents re-triggering

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered) return; // Already triggered once

        if (!other.CompareTag("Player")) return;

        hasTriggered = true; // Mark as triggered

        PlayerRespawn respawn = other.GetComponent<PlayerRespawn>();
        if (respawn != null)
        {
            respawn.SetCheckpoint(transform.position);
        }

        if (audioSource != null)
        {
            audioSource.Play();
        }

        foreach (GameObject room in roomsToEnable)
        {
            if (room != null) room.SetActive(true);
        }

        foreach (GameObject room in roomsToDisable)
        {
            if (room != null) room.SetActive(false);
        }
    }
}