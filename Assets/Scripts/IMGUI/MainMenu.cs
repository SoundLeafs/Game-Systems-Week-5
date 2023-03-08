using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
	//screen Scale will hold our x and y float values
	public Vector2 screenScale;
	//this is a true or false toggle allowing us to show/hide the grid
	public bool toggleGrid,toggleLabel;
	public bool showMenuPanel, showOptionsPanel;
	public bool testOn;
	public AudioSource audioSource;
	public Light brightness;
	//[Space(10), Header("Slider Values"),Range(0, 1)]
	//public float volume; 
	//[Range(0,1)]
	//public float brightness;
    private void Start()
    {
		audioSource = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
		brightness = GameObject.FindGameObjectWithTag("Lighting").GetComponent<Light>();
    }


    #region OnGUI - Renderer for IMGUI


    //OnGUI - Render IMGUI code and Events
    void OnGUI()
    {
		//Create a ratio of 1:1 unit that will be used on our 16:9 screen
        screenScale.x = Screen.width / 16;
        screenScale.y = Screen.height / 9;
		//Grid to display 16:9 aspect ratio
		Grid();
		Menu();
		Options();


	}
	void Grid()
	{
		//if the toggleGrid is set to true
		if(toggleGrid)
		{
			for(int x = 0; x < 16; x++)
			{
				for(int y = 0; y < 9; y++)
				{
					GUI.Box(new Rect(x*screenScale.x,y * screenScale.y,screenScale.x,screenScale.y),"");
					if(toggleLabel)
					{
						GUI.Label(new Rect(x*screenScale.x,y * screenScale.y,screenScale.x,screenScale.y),x+":"+y);	
					}
				}
			}
		}
	}

	void Options()
	{
		if (showOptionsPanel)
		{
			//Panel Background
			GUI.Box(new Rect(2f * screenScale.x, 0.5f * screenScale.y, 12f * screenScale.x, 8f * screenScale.y), "");
			//Title - Box
			GUI.Box(new Rect(4f * screenScale.x, 0.5f * screenScale.y, 8f * screenScale.x, 1.5f * screenScale.y), "Brightness");

			//Audio - Sliders
			GUI.Box(new Rect(4f * screenScale.x, 2f * screenScale.y, 8f * screenScale.x, 1.5f * screenScale.y), "Audio");
			audioSource.volume = GUI.HorizontalSlider(new Rect(4f * screenScale.x, 2.75f * screenScale.y, 8f * screenScale.x, 1.5f * screenScale.y), audioSource.volume, 0, 1);

			//Brightness - Sliders
			GUI.Box(new Rect(4f * screenScale.x, 3.5f * screenScale.y, 8f * screenScale.x, 1.5f * screenScale.y), "Quality");
			//
			brightness.intensity = GUI.HorizontalSlider(new Rect(4f * screenScale.x, 1.25f * screenScale.y, 8f * screenScale.x, 1.5f * screenScale.y), brightness.intensity, 0, 20);
			//Quality -Dropdown
			GUI.Box(new Rect(4f * screenScale.x, 5f * screenScale.y, 8f * screenScale.x, 1.5f * screenScale.y), "Resolution");
			//Resolutions -Dropdown
			GUI.Box(new Rect(4f * screenScale.x, 6.5f * screenScale.y, 8f * screenScale.x, 2f * screenScale.y), "FullScreen");
			// Enable on Build Screen.fullScreen = GUI.Toggle(new Rect(8f * screenScale.x, 7.5f * screenScale.y, 8f * screenScale.x, 2f * screenScale.y), Screen.fullScreen, "Toggle");
			testOn = GUI.Toggle(new Rect(8f * screenScale.x, 7.5f * screenScale.y, 8f * screenScale.x, 2f * screenScale.y), testOn, "Toggle");
			//Fullscreen Toggle -Boolean Toggle
			GUI.Box(new Rect(12f * screenScale.x, 0.5f * screenScale.y, 2f * screenScale.x, 8f * screenScale.y), "KeyBindings");
			
			//Keybinds -big
			if (GUI.Button(new Rect(2f*screenScale.x, 0.5f*screenScale.y, 2f*screenScale.x, 8f*screenScale.y), "Back"))
			{

				//to go back
				
				showMenuPanel = true;
				showOptionsPanel = false;
			}
		}
	}

	void Menu()
	{
		if(showMenuPanel)
		{
			//Panel
		   GUI.Box(new Rect(2f*screenScale.x,0.5f*screenScale.y,12f*screenScale.x,8f*screenScale.y),"");
		   //Title
		   GUI.Box(new Rect(4f*screenScale.x,1f*screenScale.y,8f*screenScale.x,2f*screenScale.y),"TITLE");
			//Play
			if(GUI.Button(new Rect(5f*screenScale.x,4.5f*screenScale.y,6f*screenScale.x,0.75f*screenScale.y),"Play"))
			{
				SceneManager.LoadScene(1);
			}
			//Options
			if(GUI.Button(new Rect(5f*screenScale.x,5.5f*screenScale.y,6f*screenScale.x,0.75f*screenScale.y),"Options"))
			{
				showOptionsPanel = true;
				showMenuPanel = false;
			}
			//Exit
			if(GUI.Button(new Rect(5f*screenScale.x,6.5f*screenScale.y,6f*screenScale.x,0.75f*screenScale.y),"Exit"))
			{
				#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
				#endif
				Application.Quit();
			}
		}
	}

}
#endregion