using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public UIManager UIManager;
    public CameraManager cameraManager;
    public int cameraNumber = 1;
    public GameObject GameEndUI;
    public int characterCount = 0;
    // public List<Background> backgroundList;

    private void Awake()
    {
        // Check if the instance already exists and if it's not this one
        if (Instance != null && Instance != this)
        {
            // Destroy this instance as it is a duplicate
            Destroy(gameObject);
        }
        else
        {
            // Assign the instance to this object
            Instance = this;
            // Make sure this instance persists across scene loads
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        GameStart();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            cameraNumber = 0;
            cameraManager.ChangeCameraView(cameraNumber);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            cameraNumber = 1;
            cameraManager.ChangeCameraView(cameraNumber);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            cameraNumber = 2;
            cameraManager.ChangeCameraView(cameraNumber);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            cameraNumber = 3;
            cameraManager.ChangeCameraView(cameraNumber);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            cameraNumber = 4;
            cameraManager.ChangeCameraView(cameraNumber);
        }

        if (GameObject.FindGameObjectsWithTag("Character").Length <= 0)
        {
            GameEnd();
        }
    }

    public void GameEnd()
    {
        if (GameEndUI != null)
        {
            GameEndUI.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ChangeScene(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }

    public void GameStart()
    {
        // Pick Random Character, World

        // gameObject.GetComponent<GameSetting>().characterSetting.StartGame(characterCount);
        UIManager.GameStart();
    }
}
