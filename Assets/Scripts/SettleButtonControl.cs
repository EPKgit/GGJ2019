using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettleButtonControl : MonoBehaviour
{
    public GameObject confirmationWindow;

    void Start()
    {
        if (confirmationWindow.activeSelf)
            confirmationWindow.SetActive(false);
    }

    public void SettlePressed()
    {
        confirmationWindow.SetActive(true);
    }

    public void ConfirmationPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void DeconfirmationPressed()
    {
        confirmationWindow.SetActive(false);
    }
}
