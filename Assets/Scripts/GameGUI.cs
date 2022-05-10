using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameGUI : MonoBehaviour
{
    public Optimizer _optimizer;
    public CameraController _cameraController;

    public GameManager _gameManager;

    private bool _onTitle = true;

    // Start is called before the first frame update
    void Start()
    {
        if(_optimizer == null)
        {
            _optimizer = GameObject.Find("Evaluator").GetComponent<Optimizer>();
        }

        if(_cameraController == null)
        {
            _cameraController = GameObject.Find("CameraController").GetComponent<CameraController>();
        }      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Title screen display and buttons
    void Title()
    {
        // Title
        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Surfing Neatly");

        // Play and Watch buttons
        if(GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "Play"))
        {
            _gameManager.SetPlayOrWatch(true);
            _gameManager.title = false;
        }
        if(GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 50, 200, 100), "Watch"))
        {
            _gameManager.SetPlayOrWatch(false);
            _gameManager.title = false;
        }
    }

    // Player GUI interface
    void Play()
    {
        // Reset and return to menu buttons next to each other on the botthom of the screen
        if(GUI.Button(new Rect(Screen.width - 100, Screen.height - 50, 100, 50), "Reset (R)"))
        {
            _gameManager.Reset();
        }
        if(GUI.Button(new Rect(0, Screen.height - 50, 100, 50), "Menu (M)"))
        {
            _gameManager.SetPlayOrWatch(false);
            _gameManager.toTitle();
        }
        // If they press R or M they reset or go to the menu
        if(Input.GetKeyDown(KeyCode.R))
        {
            _gameManager.Reset();
        }
        if(Input.GetKeyDown(KeyCode.M))
        {
            _gameManager.SetPlayOrWatch(false);
            _gameManager.toTitle();
        }
        // Box showing time at top of screen
        float time = _gameManager.getRunTime();
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        int milliseconds = (int)((time * 1000) % 1000);
        GUI.Box(new Rect(Screen.width / 2 - 35, 0, 70, 20), string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds));

    }
    void Watch() 
    {
        if (GUI.Button(new Rect(10, 10, 100, 40), "Start EA"))
        {
            _optimizer.StartEA();
        }
        if (GUI.Button(new Rect(10, 60, 100, 40), "Stop EA"))
        {
            _optimizer.StopEA();
        }
        if (GUI.Button(new Rect(10, 110, 100, 40), "Run best"))
        {
            _optimizer.RunBest();
        }
        if(GUI.Button(new Rect(10, 160, 100, 40), "Menu (M)"))
        {
            _optimizer.StopEA();
            _gameManager.SetPlayOrWatch(false);
            _gameManager.toTitle();
        }
        if(Input.GetKeyDown(KeyCode.M))
        {
            _gameManager.SetPlayOrWatch(false);
            _gameManager.toTitle();
        }

        GUI.Button(new Rect(Screen.width - 130, 10, 120, 40), "Time scale " + Time.timeScale);

        if(GUI.Button(new Rect(Screen.width - 130, 55, 60, 20), "+"))
        {
            _optimizer.timeScale += 1;
        }
        if(GUI.Button(new Rect(Screen.width - 70, 55, 60, 20), "-"))
        {
            _optimizer.timeScale -= 1;
        }


        GUI.Button(new Rect(10, Screen.height - 70, 100, 60), string.Format("Generation: {0}\nFitness: {1:0.00}", _optimizer.Generation, _optimizer.Fitness));

        GUI.Button(new Rect(Screen.width - 140, Screen.height - 90, 140, 30), string.Format("Camera: {0}", _cameraController.getSelectedCameraIndex()+1));

        if(GUI.Button(new Rect(Screen.width - 70, Screen.height - 30, 70, 30), "Next (C)"))
        {
            _cameraController.SelectNextCamera();
        }

        if(GUI.Button(new Rect(Screen.width - 140, Screen.height - 30, 70, 30), "Prev (V)"))
        {
            _cameraController.SelectPreviousCamera();
        }

        // GUI button for toggling free camera mode
        string camMode = _cameraController.movingCamEnabled ? "Free Cam (F) ON" : "Free Cam (F) OFF";
        if (GUI.Button(new Rect(Screen.width - 140, Screen.height - 60, 140, 30), camMode))
        {
            if(_cameraController.movingCamEnabled)
            {
                _cameraController.DisableMovingCamera();
            }
            else
            {
                _cameraController.EnableMovingCamera();
            }
        }
    }
    void OnGUI()
    {
        // If the game is in the title screen, display the title screen
        // and 'Play' or 'Watch' buttons
        if(_gameManager.title)
        {
            Title();
        }
        else
        {
            if(_gameManager.play_or_watch)
            {
                Play();
            }
            else
            {
                Watch();
            }
            
        }
    }
}
