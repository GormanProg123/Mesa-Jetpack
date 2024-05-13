using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back : MonoBehaviour
{
    public GameObject optionObject;
    public GameObject Name;
    public GameObject Gordon;
    public GameObject Startgame;
    public GameObject Options;
    public GameObject QuitGame;

    public void CancelOption()
    {
        optionObject.SetActive(false);
        Name.SetActive(true);
        Gordon.SetActive(true);
        Startgame.SetActive(true);
        Options.SetActive(true);
        QuitGame.SetActive(true);
    }
}