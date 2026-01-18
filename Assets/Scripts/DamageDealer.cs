using UnityEngine;

public class DamageDealer : MonoBehaviour
{
	[SerializeField] float _dps;

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (!collision.gameObject.CompareTag("Player"))
		{
			return;
		}

		if (collision.gameObject.TryGetComponent(out EntityHealth entityhealth))
		{
			Debug.Log("player damage");
			entityhealth.LoseHealth(Time.fixedDeltaTime * _dps);
		}
	}
}