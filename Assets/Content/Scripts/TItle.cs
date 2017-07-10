using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TItle : MonoBehaviour
{

    public GameObject PauseUI;

    private bool paused = false;

    void Start()
    {
        PauseUI.SetActive(true);
    }
    void Update()
    {
       
        if (paused)
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0;
        }
        
    }
    public void Resume()
    {

        Application.LoadLevel(1);
    }
   
}
