using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void OnClickPlayBtn()
    {
        SceneManager.LoadScene(1);
        // Scene(1);
    }
}
