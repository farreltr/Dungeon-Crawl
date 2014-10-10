using UnityEngine;

/// <summary>
/// Title screen script
/// </summary>
public class MenuScript : MonoBehaviour
{
		public GUISkin skin;
		private static string EMPTY_STRING = "";
	
		void Start ()
		{
				//in = Resources.Load ("PlayButton") as GUISkin;
		}


		void OnGUI ()
		{
				const int buttonWidth = 256;
				const int buttonHeight = 128;
				GUI.skin = skin;
				float buttonX = 4 * Screen.width / 7 - (buttonWidth / 2);
				float buttonY = (9 * Screen.height / 10) - (buttonHeight / 2);
				bool startButton = GUI.Button (new Rect (buttonX, buttonY, buttonWidth, buttonHeight), EMPTY_STRING);

				if (startButton) {
						Application.LoadLevel (1);
				}
						
		}
}