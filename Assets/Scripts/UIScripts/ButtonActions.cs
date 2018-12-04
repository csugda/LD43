﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonActions : MonoBehaviour {

    public void Play()
    {
		SceneManager.LoadScene(1);
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void AdjustVolume(float newVolume)
    {
        AudioListener.volume = newVolume;
    }
    
}
