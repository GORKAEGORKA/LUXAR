using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour
{
    public Character character;
    public Text coinsCount;
    public int coinTarget;
    public int heartTarget;
    public bool finishTarget;
    public string keyName = "S";
    public Image[] stars;

    public void Update()
    {
        if (character.finishPanel.activeSelf)
        {
            coinsCount.text = "СОБРAНО МОНЕТ: " + character.score;
            

            if (finishTarget == true && heartTarget == character.Lives && coinTarget == character.score)
            {
                stars[0].color = new Color(stars[0].color.r, stars[0].color.g, stars[0].color.b, 255);
                stars[1].color = new Color(stars[1].color.r, stars[1].color.g, stars[1].color.b, 255);
                stars[2].color = new Color(stars[2].color.r, stars[2].color.g, stars[2].color.b, 255);
                PlayerPrefs.SetInt(keyName, 3);
                PlayerPrefs.Save();
            } else if (finishTarget == true && (heartTarget == character.Lives || coinTarget == character.score)) {
                stars[0].color = new Color(stars[0].color.r, stars[0].color.g, stars[0].color.b, 255);
                stars[1].color = new Color(stars[1].color.r, stars[1].color.g, stars[1].color.b, 255);
                if (PlayerPrefs.GetInt(keyName) != 3)
                {
                    PlayerPrefs.SetInt(keyName, 2);
                    PlayerPrefs.Save();
                }
            } else if (finishTarget == true) {
                stars[0].color = new Color(stars[0].color.r, stars[0].color.g, stars[0].color.b, 255);
                if (PlayerPrefs.GetInt(keyName) != 3 && PlayerPrefs.GetInt(keyName) != 2) 
                {
                    PlayerPrefs.SetInt(keyName, 1);
                    PlayerPrefs.Save();
                }
            }
        }
    }
}
