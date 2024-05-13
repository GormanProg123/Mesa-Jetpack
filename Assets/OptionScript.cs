using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionScript : MonoBehaviour
{
    public GameObject optionObject;
    public GameObject Name;
    public GameObject Gordon;
    public GameObject Startgame;
    public GameObject Options;
    public GameObject QuitGame;

    public void Option()
    {
        optionObject.SetActive(true);
        Name.SetActive(false);
        Gordon.SetActive(false);
        Startgame.SetActive(false);
        Options.SetActive(false);
        QuitGame.SetActive(false);
    }


}
