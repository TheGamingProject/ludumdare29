using UnityEngine;
using System.Collections;

public class PointManagerScript : MonoBehaviour {
	private int currentPoints = 0; 

	void Start () {
	
	}

	void Update () {
		GameObject.Find("ScoreText").GetComponent<GUIText>().text = "Score: " + currentPoints;
	}

	public void GivePoints (int amount) {
		Debug.Log ("Points gotten: " + amount);
		currentPoints += amount;
	}
}
