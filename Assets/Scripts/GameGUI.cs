using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameGUI : MonoBehaviour
{
    public Optimizer _optimizer;
    public CameraController _cameraController;
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
    void OnGUI()
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
}
