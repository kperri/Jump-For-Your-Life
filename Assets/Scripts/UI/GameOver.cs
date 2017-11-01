using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.Collections.Generic;

public class GameOver : MonoBehaviour {

    public GameObject gameOverScreen;
	public Text newHighScore;
	public Text gameOver;
	public Text score;
	public Text finalScore; 
	public Text highScore;
	GameObject player;

	// Use this for initialization
	void Start () {
        gameOverScreen.SetActive(false);
        newHighScore.gameObject.SetActive(false);
        player = GameObject.Find ("Character");
		FindObjectOfType<CharacterMovement> ().deathEvent += Death;
	}

	void Update () {
		score.text = "Distance:" + Mathf.Round(player.transform.position.y).ToString() + "m";
	}

 //   public void startLogin () {
 //       TwitterSession session = Twitter.Session;
 //       if (session == null) {
 //           Twitter.LogIn (LoginComplete, LoginFailure);
 //       } else {
 //           LoginComplete (session);
 //       }
 //   }

 //   void LoginComplete (TwitterSession session) {
 //       startComposer (session);
 //   }

 //   void LoginFailure (ApiError error) {
 //       print ("code=" + error.code + " msg=" + error.message);
 //   }

 //   void startComposer (TwitterSession session) {
	//	Card card = new AppCardBuilder ()
	//		.GooglePlayId ("com.xregiongames.jumpforyourlife");

	//	Twitter.Compose (session, card);
	//}

	//void ShowAd () {
	//	if (Advertisement.IsReady ()) {
	//		Advertisement.Show ();
	//	}
	//}

	void Death () {
		int newHighscore = Mathf.RoundToInt (player.transform.position.y);
		int oldHighscore = DataPreservation.data._highscores[0];
        int previousScore = oldHighscore;
        int nextScore = 0;
        bool once = true;

        for (int i = 0; i < DataPreservation.data._highscores.Length - 1; i++) {

            if (once) {
                if (newHighscore > oldHighscore) {
                    newHighScore.gameObject.SetActive (true);
                    previousScore = DataPreservation.data._highscores[i];
                    DataPreservation.data._highscores[i] = newHighscore;
                } else {
                    previousScore = newHighscore;
                }
            } else if (!once && previousScore > DataPreservation.data._highscores[i]) {
                nextScore = DataPreservation.data._highscores[i];
                DataPreservation.data._highscores[i] = previousScore;
                previousScore = nextScore;
            }

            once = false;
        }
        DataPreservation.data.SaveData ();
        DataPreservation.data._deathCount += 1;
        DataPreservation.data.SaveData ();
		if (DataPreservation.data._deathCount >= 5) {
            DataPreservation.data._deathCount = 0;
            DataPreservation.data._watchedBootsAD = false;
            DataPreservation.data._watchedCoinAD = false;
            DataPreservation.data.SaveData ();
            //ShowAd ();
		}
        gameOverScreen.SetActive(true);
        player.SetActive (false);
		gameOver.text = "Game Over";
		finalScore.text = "Your score:" + Mathf.Round(player.transform.position.y).ToString() + "m";
		highScore.text = "Your best score:" + DataPreservation.data._highscores[0].ToString () + "m";
	}
}
