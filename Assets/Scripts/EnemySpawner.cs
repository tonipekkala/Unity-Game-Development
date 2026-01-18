using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class EnemySpawner : MonoBehaviour
{
	[SerializeField] List<Enemy> _enemyPrefabs;    // <-- List instead of single prefab
	[SerializeField] float _spawnCooldown;
	[SerializeField] float _spawnCooldownReductionMultiplier;
	float _currentCooldown;

	[SerializeField] Tilemap _groundTiles;
	List<Vector3> _spawnPositions = new();

	void Start()
	{
		SetEnemySpawnPositions();
		InvokeRepeating(nameof(HandleEnemySpawning), 1f, 1f);
	}

	void Update()
	{
		HandleEnemySpawning();
	}

	void HandleEnemySpawning()
	{
		_currentCooldown -= Time.deltaTime;

		if (_currentCooldown > Time.time)
			return;

		_currentCooldown = Time.time + _spawnCooldown;
		SpawnEnemyToRandomLocation();
	}

	void HandleGameDifficultyIncrease()
	{
		_spawnCooldown *= _spawnCooldownReductionMultiplier;
	}
	Vector3 GetRandomPosition()
	{
		return _spawnPositions[Random.Range(0, _spawnPositions.Count)];
	}

	void SpawnEnemyToRandomLocation()
	{
		Enemy randomEnemy = _enemyPrefabs[Random.Range(0, _enemyPrefabs.Count)];

		Instantiate(randomEnemy, GetRandomPosition(), Quaternion.identity);
	}
	void SetEnemySpawnPositions()
	{
		foreach (Vector3Int position in _groundTiles.cellBounds.allPositionsWithin)
		{
			_spawnPositions.Add(_groundTiles.GetCellCenterWorld(position));
		}
	}
}
