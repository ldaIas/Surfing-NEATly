using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

 public class CameraController : MonoBehaviour
  {
  
      public List<Camera> Cameras;

      private bool setup;

      private int selectedCameraIndex;
      void Start()
      {
           setup = false;
           DisableCameras();
           SelectCamera( 1 );
           selectedCameraIndex = 0;
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
                selectedCameraIndex = 0;
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
          }
      }
      public void SelectNextCamera()
      {
          selectedCameraIndex = (selectedCameraIndex + 1) % Cameras.Count();
          SelectCamera( selectedCameraIndex );
      }
      public void SelectPreviousCamera()
      {
          selectedCameraIndex = (selectedCameraIndex - 1 + Cameras.Count() ) % Cameras.Count();
          SelectCamera( selectedCameraIndex );
      }
      public void SelectCamera( int cameraIndex )
      {
          if( cameraIndex >= 0 && cameraIndex < Cameras.Count() )
          {
              Cameras[selectedCameraIndex].enabled = false;
              selectedCameraIndex = cameraIndex;
              Cameras[selectedCameraIndex].enabled = true;
          }
      }
 
      private void DisableCameras()
      {
          for( int i = 0 ; i < Cameras.Count() ; i++ )
              Cameras[i].enabled = false;
      }
  }