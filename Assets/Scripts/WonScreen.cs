using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WonScreen : MonoBehaviour
{
    /// <summary>
    /// changes the current scene to the scene with a matching index
    /// </summary>
    /// <param name="sceneIndex">The Index of the scene to swicth to</param>
   
    public void SwitchScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

}
