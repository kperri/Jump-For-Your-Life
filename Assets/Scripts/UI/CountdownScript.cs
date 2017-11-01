using UnityEngine;
using UnityEngine.UI;
using System;

public class CountdownScript : MonoBehaviour
{

    public Text timer;
    public Text gameOver;
    public Button mainMenu;
    public Button currentLevel;
    public Button store;
    public Image panel;
    public event Action countdownEvent;
    public event Action pauseEvent;

    bool paused;
    bool first;
    bool once;
    bool endTime;

    void Awake ()
    {
        timer.text = "3..";
        Time.timeScale = 1;
    }

    void Start ()
    {
        endTime = true;
        once = true;
    }

    void Update ()
    {
        Countdown ();
    }

    void Countdown ()
    {
        if (once && endTime)
        {
            Invoke ("SetEndTimeToFalse", 1);
            once = false;
        }
        else if (!endTime && timer.text.Equals ("3..") && !once)
        {
            ChangeText ("2..");
        }
        else if (!endTime && timer.text.Equals ("2..") && !once)
        {
            ChangeText ("1..");
        }
        else if (!endTime && timer.text.Equals ("1..") && !once)
        {
            timer.text = "";
            if (countdownEvent != null)
            {
                countdownEvent ();
            }
        }
    }

    void ChangeText (string text)
    {
        timer.text = text;
        endTime = true;
        Invoke ("SetEndTimeToFalse", 1);
    }

    void SetEndTimeToFalse ()
    {
        endTime = false;
    }

    public void Pause ()
    {
        if (timer.text.Equals ("") && gameOver.text.Equals ("Paused"))
        {
            paused = !paused;
            if (paused)
            {
                if (pauseEvent != null)
                {
                    pauseEvent ();
                }
                timer.text = "";
                gameOver.gameObject.SetActive (true);
                panel.gameObject.SetActive (true);
                Time.timeScale = 0;
                mainMenu.gameObject.SetActive (true);
                currentLevel.gameObject.SetActive (true);
                store.gameObject.SetActive (true);
            }
            else if (!paused)
            {
                gameOver.gameObject.SetActive (false);
                mainMenu.gameObject.SetActive (false);
                currentLevel.gameObject.SetActive (false);
                panel.gameObject.SetActive (false);
                store.gameObject.SetActive (false);
                timer.text = "3..";
                once = true;
                endTime = true;
                Time.timeScale = 1;
            }
        }
    }
}
