using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu_Controller : MonoBehaviour
{
    public Animator startBtn;
    public Animator quitBtn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ButtonRightHover()
    {
        quitBtn.SetTrigger("hover");
    }

   public void ButtonLeftHover()
    {
        startBtn.SetTrigger("hover");
    }

    public void ButtonRightUnHover()
    {
        quitBtn.SetTrigger("unhover");
    }

   public void ButtonLeftUnHover()
    {
        startBtn.SetTrigger("unhover");
    }
}
