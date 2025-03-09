using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;

namespace SVS.UI
{
    public class InGameMenuUI : MonoBehaviour
    {
        [SerializeField]
        LevelManagement levelManagement;
        // Start is called before the first frame update
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
          
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;

        }
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            levelManagement = FindObjectOfType<LevelManagement>();
        }
    
        public void LoadMenu()
        {
            if (levelManagement == null)
                levelManagement = FindObjectOfType<LevelManagement>();
            levelManagement.LoadMenu();
            gameObject.SetActive(false);
        }
        public void RestartLevel()
        {
            if (levelManagement == null)
                levelManagement = FindObjectOfType<LevelManagement>();
            levelManagement.RestartCurrentLevel();
            DinorSingleton.Instance.PlayerID.PlayerEvents.OnResetInputAction?.Invoke();
            gameObject.SetActive(false);
        }
    }
}