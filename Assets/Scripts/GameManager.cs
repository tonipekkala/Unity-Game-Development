using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	[SerializeField] MainMenuManager _mainMenuManager;
	[SerializeField] InGameUIManager _inGameUIManager;

	private void Awake()
	{
		if (Instance != null)
		{
			Destroy(this);
		}
		Instance = this;
		DontDestroyOnLoad(gameObject);

		Time.timeScale = 0f;
		Debug.Log(Time.timeScale + "time scale");

	}

	public void StartGame()
	{
		Time.timeScale = 1f;
		_inGameUIManager.ShowInGameUI();
	}

	public void ResetGame()
	{
		SceneManager.LoadScene(0);
	}

	public void GameOver()
	{
		Time.timeScale = 0f;
		_inGameUIManager.ShowGameOverPanel();
	}
}