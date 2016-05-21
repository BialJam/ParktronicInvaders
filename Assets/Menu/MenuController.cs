﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public List<GameObject> MainMenu;
    public List<GameObject> Scoreboard;

    public void StartGame()
    {
        SceneManager.LoadScene("City1");
    }

    public void ShowScoreboard()
    {
        foreach (var item in MainMenu)
        {
            item.SetActive(false);
        }
        foreach (var item in Scoreboard)
        {
            item.SetActive(true);
        }
        Text scores = Scoreboard[0].GetComponent<Text>();

        SaveLoad.Load();
        if (scores != null)
        {
            scores.text = "";
            for (int i = 0; i < SaveLoad.scoresSaves.Count; i++)
            {
                scores.text += string.Format("{0}. {1}\t{2}\n", i + 1, SaveLoad.scoresSaves[i].scorePoints, SaveLoad.scoresSaves[i].playerName);
            }
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        foreach (var item in Scoreboard)
        {
            item.SetActive(false);
        }
        foreach (var item in MainMenu)
        {
            item.SetActive(true);
        }
    }
}