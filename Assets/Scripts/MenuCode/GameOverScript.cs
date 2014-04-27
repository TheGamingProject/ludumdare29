using UnityEngine;

/// <summary>
/// Start or quit the game
/// </summary>
public class GameOverScript : MonoBehaviour {
	void Start () {
		
		GameObject.Find("StatusText").GetComponent<GUIText>().text = "GAME OVER";
		Debug.Log("GAME OVER");

		GameObject parentSpawner = GameObject.Find("3 - Foreground Enemies"); 
		parentSpawner.BroadcastMessage("StopSpawning");
		GameObject.Find("Stalin").GetComponent<PlayerScript>().Die();
	}

	void OnGUI() {
		const int buttonWidth = 120;
		const int buttonHeight = 60;
		
		if (
			GUI.Button(
			// Center in X, 1/3 of the height in Y
			new Rect(
			Screen.width / 2 - (buttonWidth / 2),
			(1 * Screen.height / 3) - (buttonHeight / 2),
			buttonWidth,
			buttonHeight
			),
			"Retry!"
			)
			)
		{
			// Reload the level
			Application.LoadLevel("scene1");
		}
		
		if (
			GUI.Button(
			// Center in X, 2/3 of the height in Y
			new Rect(
			Screen.width / 2 - (buttonWidth / 2),
			(2 * Screen.height / 3) - (buttonHeight / 2),
			buttonWidth,
			buttonHeight
			),
			"Back to menu"
			)
			)
		{
			// Reload the level
			Application.LoadLevel("menu");
		}
	}
}