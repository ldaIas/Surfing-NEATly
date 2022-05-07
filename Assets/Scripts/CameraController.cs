using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

 public class CameraController : MonoBehaviour
  {
      
      public static CameraController instance = null;
      public List<Camera> Cameras;

      private bool setup;

      public Camera movingCam;

      public bool movingCamEnabled {get; set;}
      private int _selectedCameraIndex;
      public int getSelectedCameraIndex()
      {
          return _selectedCameraIndex;
      }

        
      void Start()
      {
        // if it doesn't exist
        if(instance == null)
        {
            // Set the instance to the current object (this)
            instance = this;
        }
 
        // There can only be a single instance of the game manager
        else if(instance != this)
        {
            // Destroy the current object, so there is just one manager
            Destroy(gameObject);
        }
           setup = false;
           DisableCameras();
           SelectCamera( 1 );
           _selectedCameraIndex = 1;
           movingCamEnabled = false;
           if(Cameras.Count() > 0)
           {
               Cameras[1].enabled = true;
               setup = true;
           }
      }
 
      void Update()
      {
          if(!setup)
          {
                DisableCameras();
                SelectCamera( 1 );
                _selectedCameraIndex = 1;
                if(Cameras.Count() > 0)
                {
                    setup = true;
                }
          }

          if(setup)
          {
            if (Input.GetKeyDown(KeyCode.C))
            {
                SelectNextCamera();
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                SelectPreviousCamera();
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                if(movingCamEnabled)
                {
                    DisableMovingCamera();
                }
                else
                {
                    EnableMovingCamera();
                }
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                EnableMovingCamera();
                movingCam.transform.Translate(Vector3.forward * 0.1f);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                EnableMovingCamera();
                movingCam.transform.Translate(Vector3.back * 0.1f);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                EnableMovingCamera();
                movingCam.transform.Translate(Vector3.left * 0.1f);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                EnableMovingCamera();
                movingCam.transform.Translate(Vector3.right * 0.1f);
            }
          }
      }

        // Enable the moving cam from the position of the current camera
        public void EnableMovingCamera()
        {
            if(movingCamEnabled)
            {
                return;
            }
            movingCam.transform.position = Cameras[_selectedCameraIndex].transform.position;
            movingCam.transform.rotation = Cameras[_selectedCameraIndex].transform.rotation;
            Cameras[_selectedCameraIndex].enabled = false;
            movingCam.enabled = true;
            movingCamEnabled = true;           
        }

        // Restore the current camera to the position of the moving cam
        public void DisableMovingCamera()
        {
            if(!movingCamEnabled)
            {
                return;
            }
            Cameras[_selectedCameraIndex].enabled = true;
            movingCam.enabled = false;
            movingCamEnabled = false;
        }

      public void SelectNextCamera()
      {
          int selectedCameraIndex = (_selectedCameraIndex + 1) % Cameras.Count();
          SelectCamera( selectedCameraIndex );
      }
      public void SelectPreviousCamera()
      {
          int selectedCameraIndex = (_selectedCameraIndex - 1 + Cameras.Count() ) % Cameras.Count();
          SelectCamera( selectedCameraIndex );
      }
      public void SelectCamera( int cameraIndex )
      {
          if( cameraIndex >= 0 && cameraIndex < Cameras.Count() )
          {
              Cameras[_selectedCameraIndex].enabled = false;
              _selectedCameraIndex = cameraIndex;
              DisableMovingCamera();
              Cameras[_selectedCameraIndex].enabled = true;
          }
      }
 
      private void DisableCameras()
      {
        for( int i = 0 ; i < Cameras.Count() ; i++ )
            Cameras[i].enabled = false;
        movingCam.enabled = false;
      }
  }