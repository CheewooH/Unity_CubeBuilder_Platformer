using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState { MainMenu, Playing, Paused, GameOver }
    public GameState currentState { get; private set; }
    private bool playerHasMoved = false;

    public UIManager uiManager;
    public PlayerController playerController;
    public GameObject CM_MainMenuCam;
    public GameObject CM_PlayerCam;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ChangeState(GameState.MainMenu);
    }
    public void ChangeState(GameState newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case GameState.MainMenu:
                Time.timeScale = 1;
                playerHasMoved = false;
                if (CM_MainMenuCam != null) CM_MainMenuCam.SetActive(true); // ���� �޴� ī�޶� �ѱ�
                if (CM_PlayerCam != null) CM_PlayerCam.SetActive(false); // �÷��� ī�޶� ����
                CM_MainMenuCam.gameObject.SetActive(true);
                if (uiManager != null) uiManager.ShowMainMenuPanel();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case GameState.Playing:
                Time.timeScale = 1;
                if (CM_MainMenuCam != null) CM_MainMenuCam.SetActive(false); // ���� �޴� ī�޶� ����
                if (CM_PlayerCam != null) CM_PlayerCam.SetActive(true); // �÷��� ī�޶� �ѱ�
                if (uiManager != null) uiManager.ShowGamePanel();
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            case GameState.GameOver:
                Time.timeScale = 0;
                if (CM_MainMenuCam != null) CM_MainMenuCam.SetActive(false); // ���� �޴� ī�޶� ����
                if (CM_PlayerCam != null) CM_PlayerCam.SetActive(true); // �÷��� ī�޶� �ѱ�
                if (uiManager != null) uiManager.ShowGameOverPanel();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case GameState.Paused:
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
        }
    }

    public void PlayerHasMoved()
    {
        if (currentState == GameState.Playing && !playerHasMoved)
        {
            playerHasMoved = true;
            Debug.Log("�÷��̾� ù ������ ����!");
        }
    }

    public void StartGame()
    {
        ChangeState(GameState.Playing);
        if (playerController != null)
        {
            playerController.ResetPlayer();
        }
    }

    public void GameFinished()
    {
        if (currentState == GameState.Playing)
        {
            ChangeState(GameState.GameOver);
        }
    }

    public void GoToMainMenu()
    {
        ChangeState(GameState.MainMenu);
    }

    public void RestartGame()
    {
        playerHasMoved = false;
        if (playerController != null)
        {
            playerController.ResetPlayer();
        }
        ChangeState(GameState.Playing);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}