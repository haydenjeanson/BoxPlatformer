using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfGameRestart : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 || Input.GetKeyDown("r"))
            SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }
}

