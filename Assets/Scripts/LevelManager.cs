using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Text levelText;
    private int currentLevel;

    void Start()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex + 1;
        UpdateLevelText();
    }

    void UpdateLevelText()
    {
        levelText.text = "Level " + currentLevel;
    }

    public void LoadLevel(int levelIndex)
    {
        if (levelIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(levelIndex);
        }
        else
        {
            Debug.Log("No more levels to load!");
        }
    }
}
