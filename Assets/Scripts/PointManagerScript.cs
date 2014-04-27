using UnityEngine;
using System.Collections;

public class PointManagerScript : MonoBehaviour {
	private int currentPoints = 0; 

	private int highscore;

	void Start () {
		if (PlayerPrefs.HasKey("HighScore")) {
			highscore = PlayerPrefs.GetInt("HighScore");
		}
	}

	void Update () {
		GameObject.Find("ScoreText").GetComponent<GUIText>().text = "Score: " + currentPoints;
		
		GameObject.Find("HighScoreText").GetComponent<GUIText>().text = "HighScore: " + currentPoints;
	}

	public void GivePoints (int amount) {
		//Debug.Log ("Points gotten: " + amount);
		currentPoints += amount;

		checkNewHighscore();
	}

	void checkNewHighscore () {
		if (currentPoints > highscore) {
			highscore = currentPoints;
			PlayerPrefs.SetInt("HighScore", currentPoints);
		}
	}


}
