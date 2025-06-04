using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI 패널")]
    public GameObject mainMenuPanel;
    public GameObject gamePanel;
    public GameObject gameOverPanel;

    [Header("Game Over UI")]
    public TextMeshProUGUI gameOverText;
    private void HideAllPanels()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (gamePanel != null) gamePanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
    }

    public void ShowMainMenuPanel()
    {
        HideAllPanels();
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
    }

    public void ShowGamePanel()
    {
        HideAllPanels();
        if (gamePanel != null) gamePanel.SetActive(true);
    }
    public void ShowGameOverPanel()
    {
        HideAllPanels();
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        if (gameOverText != null)
        {
            gameOverText.text = "게임 완료!";
        }
    }
    public void OnStartGameButtonPressed()
    {
        if (GameManager.Instance != null) GameManager.Instance.StartGame();
    }

    public void OnGoToMainMenuButtonPressed()
    {
        if (GameManager.Instance != null) GameManager.Instance.GoToMainMenu();
    }

    public void OnRestartGameButtonPressed()
    {
        if (GameManager.Instance != null) GameManager.Instance.RestartGame();
    }

    public void OnQuitGameButtonPressed()
    {
        if (GameManager.Instance != null) GameManager.Instance.QuitGame();
    }
}
