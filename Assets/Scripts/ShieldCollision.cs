using UnityEngine;

public class ShieldCollision : MonoBehaviour
{
    [SerializeField] AudioClip _enemyhitSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering the bubble has the "Enemy" tag
        if (other.CompareTag("Enemy"))
        {
            // Destroy the enemy object
            Destroy(other.gameObject);

            Debug.Log("Enemy vaporized!");
            AudioManager.Instance.PlayAudio(_enemyhitSound, AudioManager.SoundType.SFX, 1.0f, false);

        }
    }
}