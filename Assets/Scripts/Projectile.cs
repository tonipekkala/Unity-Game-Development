using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float _travelSpeed;
    [SerializeField] float _damage;
    [SerializeField] ParticleSystem _hitParticle;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] AudioClip _enemyhitSound;

    public void InitializeProjectile(Vector2 direction)
    {
        Launch(direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            DestroyProjectile();
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            DealDamage(collision.gameObject);
            DestroyProjectile();
        }
    }

    void DealDamage(GameObject target)
    {
        if (target.TryGetComponent(out EntityHealth entityhealth))
        {
            entityhealth.LoseHealth(_damage);
            AudioManager.Instance.PlayAudio(_enemyhitSound, AudioManager.SoundType.SFX, 1.0f, false);
        }
    }

    void Launch(Vector2 direction)
    {
        Vector2 movement = direction.normalized * _travelSpeed;
        _rb.linearVelocity = movement;
    }

    void DestroyProjectile()
    {
        ParticleSystem hitParticles = Instantiate(_hitParticle, transform.position, Quaternion.identity);
        Destroy(hitParticles.gameObject, 1f);
        Destroy(gameObject);
    }
}
