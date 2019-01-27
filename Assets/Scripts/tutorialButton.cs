using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialButton : MonoBehaviour
{
    public GameObject canvas; 
    public void openTutorial()
    {
        canvas.SetActive(true);
    }
}
