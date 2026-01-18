using System;
using UnityEngine;
using System.Threading.Tasks;

public class EntityHealth : MonoBehaviour
{
	[SerializeField] float _maxHealth;
	[SerializeField] float _currentHealth;
	[SerializeField] float _healthRegen;
	//[SerializeField] private Animator _animator;

	public Action OnDeath;
	public Action<float, float> OnHealthChanged;
	private void Awake()
	{
		_currentHealth = _maxHealth;
	}

	public void LoseHealth(float healthLost)
	{
		_currentHealth -= healthLost;
		OnHealthChanged?.Invoke(Mathf.Clamp(_currentHealth, 0, _maxHealth), _maxHealth);

		if (_currentHealth <= 0)
		{
			//_animator.SetBool("isDead", true);
			Death();
		}
	}

	public async void Death()
	{
		await Task.Delay(3000);
		OnDeath?.Invoke();
	}

	void HandleHealthRegen()
	{
		_currentHealth = Mathf.Clamp(_currentHealth + _maxHealth * _healthRegen, 0, _maxHealth);
		OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
	}

	void Start()
	{
		InvokeRepeating(nameof(HandleHealthRegen), 1f, 1f);
	}

}
