using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var ingameMenuToggle    = boolean = false;
        var helpMenuToggle      = boolean = false;
        var scoreboardToggle    = boolean = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown (KeyCode.Escape ))
        {
            EscapePressed();
        }
    
        if (Input.GetKeyDown (KeyCode.P)) // Helpmenu == H button in key setup
        {
            HelpMenuCall();
        }
    
        if (Input.GetButtonDown ("scoreboard")) // Helpmenu == tab button in key setup
        {
            ScoreBoardCall();
        }
    }

    void HelpMenuCall() {

        if (!helpMenuToggle)
        {
            helpMenuToggle = true;
            Screen.lockCursor = false;
        }

    }

    void ShowBookMenu() {

        GUI.BeginGroup (Rect ((scaledResolutionWidth / 2) -128,( Screen.height / 2) - 192, 256, 384));
            //GUI.Box (Rect (0,0,256,384), "Ingame menu");  // Make the group visible.
        
        GUI.Label(Rect (0,0,256,384), menuBG);  // menuBG  is a background texture
    
        // RESUME GAME
        if (GUI.Button( Rect (38,50,180,45), "Resume game"))
        {
            helpMenuToggle = false ;
            Screen.lockCursor = true;
        }
    }

    void OnGUI() {

        GUI.skin = guiSkin; // Set up gui skin
        // GUI is laid out for a 1920 x 1200 pixel display (16:10 aspect). The next line makes sure it rescales nicely to other resolutions.
        GUI.matrix = Matrix4x4.TRS (Vector3(0, 0, 0), Quaternion.identity, Vector3 (Screen.height / nativeVerticalResolution, Screen.height / nativeVerticalResolution, 1));

        switch (menuLevel) {
            case StartMenu:
                //Your GUI Information
            break;
    
            case PauseMenu:
                //Your GUI Information
            break;
    
            case helpMenuToggle:
                ShowBookMenu();
            break;
        }
    }

}



