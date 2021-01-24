using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

public class PlayerController : MonoBehaviour {
 
    public float MoveSpeed = 8f;
    public float jumpSpeed = 7f;
    private float MouseSpeed = 2f;
    public float maxVelocity = .1f;
    private int spawnX = 0;
    private int spawnY = 2;
    private int spawnZ = 0;
    bool isGrounded;
 
    //to keep our rigid body
    Rigidbody Rigid;
 
    //to keep the collider object
    Collider coll;
 
    //flag to keep track of whether a jump started
    bool pressedJump = false;
 
    // Use this for initialization
    void Start () {
        //Set start position
        this.transform.position = new Vector3(spawnX, spawnY, spawnZ);

        //get the rigid body component for later use
        Rigid = GetComponent<Rigidbody>();
 
        //get the player collider
        coll = GetComponent<Collider>();

        Cursor.lockState = CursorLockMode.Locked;
    }
 
    // Update is called once per frame
    void Update ()
    {
        // Handle player walking
        WalkHandler();
 
        //Handle player jumping
        JumpHandler();

        //Handle restarting
        CheckRestart();
    }
 
    //Check if we need to restart
    void CheckRestart()
    {
        if(transform.position.y < -14)
        {
            Vector3 restartPos = new Vector3(spawnX, spawnY, spawnZ);
            Rigid.MovePosition(restartPos);
        }
    }

    // Make the player walk according to user input
    void WalkHandler()
    {
        //X rotation
        Rigid.MoveRotation(Rigid.rotation * Quaternion.Euler(new Vector3(0, Input.GetAxis("Mouse X") * MouseSpeed, 0)));

        //Keyboard movement
        //Rigid.MovePosition(transform.position + (transform.forward * Input.GetAxis("Vertical") * MoveSpeed) + (transform.right * Input.GetAxis("Horizontal") * MoveSpeed));
        print("vertical");
        print(transform.forward * Input.GetAxis("Vertical") * MoveSpeed);
        print("horizontal");
        print(transform.right * Input.GetAxis("Horizontal") * MoveSpeed);
        Vector3 verticalVel = transform.forward * Input.GetAxis("Vertical") * MoveSpeed;
        Vector3 horizontalVel = transform.right * Input.GetAxis("Horizontal") * MoveSpeed;

        //If in the starting area, limit max velocity
        if(transform.position.x < 10 && transform.position.x > -10 && transform.position.y > 0 && transform.position.z < 9 && transform.position.z > -10)
        {
            Rigid.velocity = Vector3.ClampMagnitude(Rigid.velocity, maxVelocity);
            Rigid.velocity += verticalVel + horizontalVel;
        }
        Rigid.velocity += horizontalVel;

    }
 
    // Check whether the player can jump and make it jump
    void JumpHandler()
    {
        // Jump axis
        float jAxis = Input.GetAxis("Jump");
        isGrounded = CheckGrounded();
 
        // Check if the player is pressing the jump key
        if (jAxis > 0f)
        {
            // Make sure we've not already jumped on this key press
            if(!pressedJump && isGrounded)
            {
                // We are jumping on the current key press
                pressedJump = true;
 
                // Jumping vector
                Vector3 jumpVector = new Vector3(0f, jumpSpeed, 0f);
 
                // Make the player jump by adding velocity
                Rigid.velocity = Rigid.velocity + jumpVector;
            }            
        }
        else
        {
            // Update flag so it can jump again if we press the jump key
            pressedJump = false;
        }
    }

    // Check if the object is grounded
    bool CheckGrounded()
    {
        // Object size in x
        float sizeX = coll.bounds.size.x;
        float sizeZ = coll.bounds.size.z;
        float sizeY = coll.bounds.size.y;
 
        // Position of the 4 bottom corners of the game object
        // We add 0.01 in Y so that there is some distance between the point and the floor
        Vector3 corner1 = transform.position + new Vector3(sizeX/ 2, -sizeY / 2 + 0.2f, sizeZ / 2);
        Vector3 corner2 = transform.position + new Vector3(-sizeX / 2, -sizeY / 2 + 0.2f, sizeZ / 2);
        Vector3 corner3 = transform.position + new Vector3(sizeX / 2, -sizeY / 2 + 0.2f, -sizeZ / 2);
        Vector3 corner4 = transform.position + new Vector3(-sizeX / 2, -sizeY / 2 + 0.2f, -sizeZ / 2);
 
        // Send a short ray down the cube on all 4 corners to detect ground
        bool grounded1 = Physics.Raycast(corner1, new Vector3(0, -1, 0), 0.2f);
        bool grounded2 = Physics.Raycast(corner2, new Vector3(0, -1, 0), 0.2f);
        bool grounded3 = Physics.Raycast(corner3, new Vector3(0, -1, 0), 0.2f);
        bool grounded4 = Physics.Raycast(corner4, new Vector3(0, -1, 0), 0.2f);
 
        // If any corner is grounded, the object is grounded
        return (grounded1 || grounded2 || grounded3 || grounded4);
    }
}
