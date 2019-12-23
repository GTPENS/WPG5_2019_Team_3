using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuBtn : MonoBehaviour
{
    public void StartBtn()
    {
        SceneManager.LoadScene("Gameplay");
    }
    public void CreditSbtn()
    {
        SceneManager.LoadScene("Credit");
    }

    public void BackBtn()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitBtn()
    {
        Application.Quit();
    }
}
