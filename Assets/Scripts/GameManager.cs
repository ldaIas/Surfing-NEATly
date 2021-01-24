using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Fragsurf.Movement;

public class GameManager : MonoBehaviour
{
	public static GameManager instance = null;
	
    private GameObject playerObj = null;
    private GameObject ramp = null;
	
    // Called when the object is initialized
    void Awake()
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

        // Don't destroy this object when loading scenes
        DontDestroyOnLoad(gameObject);
    }

    //Get this instance
    public GameManager getInstance()
    {
        return this;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
