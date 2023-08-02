using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinnerCanvas : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("Menu");
    }
}
