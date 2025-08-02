using UnityEngine;

public class TilemapTriggerActivator : MonoBehaviour
{
    [Header("Tilemap GameObject to Activate")]
    public GameObject tilemapToActivate;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && tilemapToActivate != null)
        {
            tilemapToActivate.SetActive(true);
        }
    }
}
