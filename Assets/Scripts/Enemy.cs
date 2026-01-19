using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] AudioClip _deathSound;

    EntityHealth _entityHealth;

    NavMeshAgent _agent;
    GameObject _target;
    void Awake()
    {
        _entityHealth = GetComponent<EntityHealth>();

        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
    }

    public void DestroyEnemy()
    {
        AudioManager.Instance.PlayAudio(_deathSound, AudioManager.SoundType.SFX, 1.0f, false);
        Destroy(gameObject);
    }

    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player");

        _entityHealth.OnDeath += DestroyEnemy;
    }

    // Update is called once per frame
    void Update()
    {
        // Only set destination if the agent is actually on a mesh
        if (_agent.isOnNavMesh)
        {
            _agent.SetDestination(_target.transform.position);
        }
    }
    private void OnDisable()
    {
        _entityHealth.OnDeath -= DestroyEnemy;
    }
}
