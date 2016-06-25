using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public void OnClickStart()
    {
        Application.LoadLevel("Level_1");
    }
    public void OnClickExit()
    {
        Application.Quit();
    }
}
