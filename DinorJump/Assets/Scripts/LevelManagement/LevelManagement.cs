using SVS.Level;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class LevelManagement : MonoBehaviour
{
    [SerializeField]
    private int level_1SceneBuildIndex, menuSceneBuildIndex, winSceneBuildIndex;

    [SerializeField]
    private bool ResetSaveData = false;

    private void Awake()
    {
        if (ResetSaveData)
        {
            SaveSystem.ResetSaveData();
        }
    }

    public void RestartCurrentLevel()
    {
        LoadSceneWithIndex(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadStartLevel()
    {
        LoadSceneWithIndex(level_1SceneBuildIndex);
    }
    public void LoadNextLevel()
    {
        LoadSceneWithIndex(GetNextLevelIndex());
    }
    public void LoadMenu()
    {
        LoadSceneWithIndex(menuSceneBuildIndex);
    }
    public void LoadWinScene()
    {
        LoadSceneWithIndex(winSceneBuildIndex);
    }
    public int GetNextLevelIndex()
    {
        int index = SceneManager.GetActiveScene().buildIndex + 1;
        if (index < SceneManager.sceneCountInBuildSettings)
        {
            return index;
        }
        else
        {
            return winSceneBuildIndex;
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR    
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void LoadSceneWithIndex(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
}
