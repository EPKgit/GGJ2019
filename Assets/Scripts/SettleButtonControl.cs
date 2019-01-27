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
        Debug.Log("Settle confirmed. Going to EndScreen");
        SceneManager.LoadScene("EndScreen");
    }

    public void DeconfirmationPressed()
    {
        confirmationWindow.SetActive(false);
    }
}
