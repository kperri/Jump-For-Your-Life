using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScenes : MonoBehaviour {

	public Text highscore;

	Scene scene;
    string scoreList;

	void Start () {
		scene = SceneManager.GetActiveScene ();
        for (int i = 0; i < DataPreservation.data._highscores.Length - 1; i++) {
            scoreList += (i + 1).ToString () + ". " + DataPreservation.data._highscores[i].ToString () + "m\n";
        }
        if (scene.name.Equals ("TitleScreen")) {
            highscore.text = "Your Highscores:" + "\n" + scoreList;
        } else {
            highscore.text = "New Highscore!";
        }
	}

	public void LoadTitleScreen () {
		SceneManager.LoadScene ("TitleScreen");
	}

	public void LoadLevel () {
		SceneManager.LoadScene (scene.name);
		Time.timeScale = 1;
	}

	public void Endless () {
		SceneManager.LoadScene ("Endless");
	}

	public void OpenTwitter () {
		Application.OpenURL ("https://twitter.com/XregionGames");
	}

	public void OpenFacebook () {
		Application.OpenURL ("https://www.facebook.com/xregiongames/");
	}

    public void OpenInstagram () {
        Application.OpenURL ("https://www.instagram.com/xregiongames/");
    }

}
