using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    private Character character;
    public GameObject levelChanger;
    public GameObject exitPanel;
    public GameObject informationPanel;
    public int nextLevel;
  
    void Update()
    {
        if ((levelChanger.activeSelf == true || informationPanel.activeSelf == true) && Input.GetKeyDown(KeyCode.Escape))
        {
            levelChanger.SetActive(false);
            informationPanel.SetActive(false);
        } else if (exitPanel.activeSelf == false && Input.GetKeyDown(KeyCode.Escape))
        {
            exitPanel.SetActive(true);
        } else if (Input.GetKeyDown(KeyCode.Escape))
        {
            exitPanel.SetActive(false);
        }
    }

    public void OnClickStart()
    {
        levelChanger.SetActive(true);
    }

    public void OnClickExit()
    {
        Application.Quit();
    }

    public void ExitButton()
    {
        exitPanel.SetActive(true);
    }

    public void No()
    {
        exitPanel.SetActive(false);
    }

    public void levelButton(int level)
    {
        SceneManager.LoadScene(level);
        
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
        
    }
    
    public void NextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }
    public void OnClickInformation()
    {
        informationPanel.SetActive(true);
    }
    
    public void Ok()
    {
        informationPanel.SetActive(false);
    }
}
