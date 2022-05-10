using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

 public class CameraController : MonoBehaviour
  {
      
      public static CameraController instance = null;
      public GameManager gameManager;
      public List<Camera> Cameras;
      public Camera movingCam;
      public Camera TitleCam;
      public bool movingCamEnabled {get; set;}
      private float camMoveSpeed = 2f;
      private CharacterController controller;
      private Vector3 camVelocity;
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
        DisableCameras();
        movingCamEnabled = false;
        _selectedCameraIndex = 0;

        controller = gameObject.AddComponent<CharacterController>();
      }
 
      void Update()
      {
          if(gameManager.title)
          {
                TitleCam.enabled = true;
          }
          else
          {
            if(!gameManager.play_or_watch)
            {
                TitleCam.enabled = false;
                SelectCamera(_selectedCameraIndex);
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
                ProcessMovement();
            }
            else
            {
                DisableCameras();
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


        // Process the movement of the camera
        public void ProcessMovement()
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            controller.Move(move * camMoveSpeed * Time.deltaTime);
            if(move != Vector3.zero)
            {
                movingCamEnabled = true;
                gameObject.transform.forward = move;
            }
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
        TitleCam.enabled = false;
      }
  }