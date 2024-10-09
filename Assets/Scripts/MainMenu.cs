using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text levelText; 
    private int currentLevel = 0;
    void Start()
    {
        UpdateLevelText();
    }

    void UpdateLevelText()
    {
        levelText.text = "Level " + currentLevel;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1); 
    }

}
