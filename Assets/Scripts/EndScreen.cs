using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Delgado, Angela
 * 4/21/2014
 * This Script helps transport to game over screen when charcter looses all health
 */
public class EndScreen : MonoBehaviour
{
    
    /// <summary>
    /// Quits Game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    ///<summary>
    ///Changes the current scene to the scene with a matching index
    /// </summary>
    /// <parem name ="sceneIndex">The Index of the scene to switch to</parem>
    public void SwitchScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
