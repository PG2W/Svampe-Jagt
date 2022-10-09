using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BookMenu : MonoBehaviour
{
    
    //bool startMenuToggle     = false;
    bool helpMenuToggle      = false;
    //bool pauseMenuToggle     = false;

    [SerializeField] GameObject helpMenu;
    
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
    
        if (Input.GetKeyDown (KeyCode.P) && !helpMenuToggle) // Book Menu
        {
            helpMenuToggle = true;
            Cursor.lockState = CursorLockMode.None;
            helpMenu.SetActive(true);
        }
    
         if (Input.GetKeyDown (KeyCode.P) && helpMenuToggle)
        {
            helpMenuToggle = false ;
            Cursor.lockState = CursorLockMode.Locked;
            helpMenu.SetActive(false);
        }

        if (Input.GetButtonDown ("scoreboard")) 
        {
            
        }
    }



    /*public void ShowBookMenu() {

       helpMenu.SetActive(true);
       
    }

    void OnGUI() {

        if (startMenuToggle) {

        }
    
        if (pauseMenuToggle) {

        }

        if (helpMenuToggle) {
            ShowBookMenu();
        }
                
            
        }*/
}





