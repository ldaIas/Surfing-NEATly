using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Fragsurf.Movement;

public class GameManager : MonoBehaviour
{
	public static GameManager instance = null;
	
    public SurfCharacter _playerCharacter;
    public Optimizer _optimizer;
    public CameraController _cameraController;
    public GameObject _spawnPoint;

    [HideInInspector]
    public bool title = true;
    [HideInInspector]
    public bool play_or_watch = false;
    private float _runTime = 0.0f;
	
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
        _playerCharacter.enabled = false;
        _playerCharacter.transform.localScale = new Vector3(0, 0, 0);
    }

    //Get this instance
    public GameManager getInstance()
    {
        return this;
    }

    // Update is called once per frame
    void Update()
    {
        if(play_or_watch)
        {
            _runTime += Time.deltaTime;
        }
    }

    // Set play_or_watch based on selection
    public void SetPlayOrWatch(bool play_or_watch)
    {
        this.play_or_watch = play_or_watch;
        if(play_or_watch)
        {
            _playerCharacter.enabled = true;
            _playerCharacter.transform.localScale = new Vector3(1, 1, 1);
            _playerCharacter.transform.position = _spawnPoint.transform.position;
            _playerCharacter.transform.rotation = _spawnPoint.transform.rotation;
            _optimizer.enabled = false;
        }
        else
        {
            _playerCharacter.enabled = false;
            _optimizer.enabled = true;
        }
    }

    public void toTitle()
    {
        title = true;
        play_or_watch = false;
        _playerCharacter.enabled = false;
        _playerCharacter.transform.localScale = new Vector3(0, 0, 0);
    }

    // Reset the game if playing
    public void Reset()
    {
        if(play_or_watch)
        {
            _playerCharacter.transform.position = _spawnPoint.transform.position;
            _playerCharacter.viewTransform.rotation = _spawnPoint.transform.rotation;
            _runTime = 0.0f;
        }
    }

    public float getRunTime()
    {
        return _runTime;
    }
}
