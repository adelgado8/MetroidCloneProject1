using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
   public PlayerControl PlayerController;
   public TMP_Text Healthtext;

    // Update is called once per frame
    void Update()
    {
        Healthtext.text = "health: " + PlayerController.health;
    }
}
