using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeSceneButton : MonoBehaviour
{
	[SerializeField] private Button button;
	[SerializeField] private string sceneName;

	private void OnEnable()
		=> button.onClick.AddListener(LoadScene);

	private void OnDisable()
        => button.onClick.RemoveListener(LoadScene);

	private void LoadScene()
	{
		SceneManager.LoadScene(sceneName);
	}
}
