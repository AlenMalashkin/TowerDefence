using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private GameObject pausePanel;

    [Header("Pause panel buttons")] 
    [SerializeField] private Button pausePanelPlayButton;

    private void OnEnable()
    {
        Time.timeScale = 1;
        pauseButton.onClick.AddListener(PauseGame);
        pausePanelPlayButton.onClick.AddListener(UnpauseGame);
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        pauseButton.gameObject.SetActive(false);
    }

    private void UnpauseGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        pauseButton.gameObject.SetActive(true);
    }
}
