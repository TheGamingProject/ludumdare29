using UnityEngine;

/// <summary>
/// Title screen script
/// </summary>
public class MenuScript : MonoBehaviour {
/*	void OnGUI () {
		const int buttonWidth = 84;
		const int buttonHeight = 60;
		
		// Draw a button to start the game
		if (
			GUI.Button(
			// Center in X, 2/3 of the height in Y
			new Rect(
			Screen.width / 2 - (buttonWidth / 2),
			(Screen.height / 3) - (buttonHeight / 2),
			buttonWidth,
			buttonHeight
			),
			"Start!"
			)
			)
		{
			Application.LoadLevel("scene1");
		}
	}
*/
	void Update () {
		if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2")) {
			Application.LoadLevel("scene1");
		}
	}
}