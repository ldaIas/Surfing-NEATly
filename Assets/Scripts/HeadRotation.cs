using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadRotation : MonoBehaviour
{

    Rigidbody Rigid;
    private float MouseSpeed = 2f;
    public float moveSpeed = 2.0f;
 
    public float minTurnAngle = -90.0f;
    public float maxTurnAngle = 90.0f;
    private float rotX;

    // Start is called before the first frame update
    void Start()
    {
        //get the rigid body component for later use
        Rigid = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        CheckRotate();
    }

    void CheckRotate()
    {

        // get the mouse inputs
        float y = Input.GetAxis("Mouse X") * MouseSpeed;
        rotX += Input.GetAxis("Mouse Y") * MouseSpeed;
    
        // clamp the vertical rotation
        //rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);
    
        // rotate the camera
        transform.eulerAngles = new Vector3(-rotX, transform.eulerAngles.y + y, 0);
        //X rotation
        //Rigid.MoveRotation(Rigid.rotation * Quaternion.Euler(new Vector3(0, Input.GetAxis("Mouse X") * MouseSpeed, 0)));
        
        //Y rotation
        //Rigid.MoveRotation(Rigid.rotation * Quaternion.Euler(new Vector3( -(Input.GetAxis("Mouse Y") * MouseSpeed), 0, 0)));
    }
}
