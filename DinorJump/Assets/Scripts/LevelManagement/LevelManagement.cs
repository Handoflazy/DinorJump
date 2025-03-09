    using System.Collections;
    using SVS.Level;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagement : MonoBehaviour
{  
    [SerializeField]
    private int level1SceneBuildIndex;

    [SerializeField]
    private int menuSceneBuildIndex, winSceneBuildIndex;

    [SerializeField]
    private bool ResetSaveData = false;

    private void Awake()
    {
        if (ResetSaveData)
        {
            SaveSystem.ResetSaveData();
        }
    }
    public void RestartCurrentLevel() {
        //Destroy(FindObjectOfType<Player>().gameObject);
        LoadSceneWithIndex(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator DelayCoroutine(float t,int sceneIndex) {
        yield return new WaitForSeconds(t);
        LoadSceneWithIndex(sceneIndex);
    }
    public void LoadStartLevel()
    {
        PlayerPrefs.DeleteAll();
        StartCoroutine(DelayCoroutine(0f, level1SceneBuildIndex));
    }
    public void LoadNextLevel()
    {
        Debug.Log("Loading next level");
        StartCoroutine(DelayCoroutine(0.2f,GetNextLevelIndex()));
    }
    public void LoadMenu()
    {
        foreach (var obj in GameObject.FindObjectsOfType<GameObject>())
        {
            if (obj.scene.name == "DontDestroyOnLoad")
            {
                Destroy(obj);
            }
        }
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

        return winSceneBuildIndex;
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
