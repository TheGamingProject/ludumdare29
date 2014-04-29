using UnityEngine;
using System.Collections;

public class PointManagerScript : MonoBehaviour {
	private int currentPoints = 0; 
	
	private int highscore;
	
	void Start () {
		if (PlayerPrefs.HasKey("HighScore")) {
			highscore = PlayerPrefs.GetInt("HighScore");
		}

		//SubmitStats(false);
	}
	
	void Update () {
		//GameObject.Find("ScoreText").GetComponent<GUIText>().text = "Score: " + currentPoints;
		
		//GameObject.Find("HighScoreText").GetComponent<GUIText>().text = "HighScore: " + currentPoints;
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
	
	public int GetCurrentPoints () {
		return currentPoints;
	}

	private string url = "zion.tgp.io:10091/mashed";
	public void SubmitStats (bool isWin) {
		/*		
		HTTPRequest request = new HTTPRequest(new System.Uri("http://google.com"), HTTPMethods.Get, OnRequestFinished);
		
		//HTTPRequest request = new HTTPRequest(new System.Uri(url), OnRequestFinished);
		
		//request.AddField("iswin", "" + isWin);
		//request.AddField("killed", "" + currentPoints);

		Debug.Log("sent");
		request.Send();

		HTTPManager.SendRequest("http://google.com", OnRequestFinished);

		//HTTPManager.SendRequest(request);

		//HTTPManager.
		*/
		
	}
	
	//void OnRequestFinished(HTTPRequest request, HTTPResponse response) {
	//	Debug.Log("Request Finished! Text received: ");// + response.DataAsText);
	//	Debug.Log(response);
		/*
			if ( !result )

		{
			Debug.LogWarning( "Could not parse JSON response!" );
			return;
		}
		
		int total = 9001;
		
		if (thing.ContainsKey("totalMashed")) {
			total = (int) thing["totalMashed"];
		} 
		*/
		//GameObject.Find("ScoreText").GetComponent<GUIText>().text = "Total Mashed: " + response.DataAsText;
	//}

}
