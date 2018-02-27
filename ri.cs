using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ri : MonoBehaviour {

    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {

    }

    void LoadGameScene()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }
    }

    // Update is called once per frame
    void Update ()
    {
        LoadGameScene();	
	}
}
