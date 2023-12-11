using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using TMPro;
public class tutorialManager : MonoBehaviour
{
    private int current;
    public GameObject[] scenes;
    public GameObject button = null;
    public GameObject block = null;
    public Shopping shop = null;
    public int levelNumber;
    public HordeManager wave = null;
    public static tutorialManager instance;
    public int flag;

    // Start is called before the first frame update
    void Start()
    {
        current = 0;
        flag = 0;
    }

    void Update()
    {
        // Debug.Log(current);
        if (levelNumber == 1)
        {
            if (button != null && shop.getGold() == 0)
            {
                button.SetActive(true);
            }
            if (HordeManager.instance.Wave() == 1 && flag == 0)
            {
                scenes[current].SetActive(false);
                current++;
                flag = 1;
            }
            if (shop.getGold() == 15)
            {
                scenes[current].SetActive(true);
            }
            if (current == 7)
            {
                scenes[current].SetActive(false);
            }
            if (current == 7 && shop.getGold() > 300000 && levelNumber == 1 && block != null)
            {
                scenes[current].SetActive(true);
            }
        }
    }
    public void hide()
    {
        scenes[current].SetActive(false);
    }
    public void loadNext()
    {
        if (current < scenes.Length) scenes[current].SetActive(false);
        current++;
        if (current < scenes.Length) scenes[current].SetActive(true);
    }
}