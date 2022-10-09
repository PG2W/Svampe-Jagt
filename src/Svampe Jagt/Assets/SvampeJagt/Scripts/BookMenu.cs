using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookMenu : MonoBehaviour
{
    
    bool startMenuToggle     = false;
    bool helpMenuToggle      = false;
    bool pauseMenuToggle     = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown (KeyCode.Escape))
        {
            
        }
    
        if (Input.GetKeyDown (KeyCode.P)) // Helpmenu == H button in key setup
        {
            HelpMenuCall();
        }
    
        if (Input.GetButtonDown ("scoreboard")) // Helpmenu == tab button in key setup
        {
            
        }
    }

    void HelpMenuCall() {

        if (!helpMenuToggle)
        {
            helpMenuToggle = true;
            Cursor.lockState = CursorLockMode.None;
        }

    }

    void ShowBookMenu() {

        
    }

    void OnGUI() {

        if (startMenuToggle) {

        }
    
        if (pauseMenuToggle) {

        }

        if (helpMenuToggle) {
            ShowBookMenu();
        }
                
            
        }
}





