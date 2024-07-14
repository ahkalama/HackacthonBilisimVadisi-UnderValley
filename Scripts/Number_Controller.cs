using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Number_Controller : MonoBehaviour
{
    public static Number_Controller Instance;
    public AudioSource kucukbuyuksong;

    public GameObject lambadoor;
    public GameObject lambalight;
    public Material yesilisik;

    public int correctCounter = 0;
    public int wrong = 0;
    public int stepCounter = 0;
    public int isEverythingCorrect = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void IncrementCorrectCounter()
    {
        correctCounter++;
        Debug.Log("Correct Counter: " + correctCounter);
    
    }
    public void DecrementCorrectCounter()
    {
        correctCounter--;
        Debug.Log("Correct Counter: " + correctCounter);
    }

    public void IncrementWrongCounter()
    {
        wrong++;
        Debug.Log("Wrong Counter: " + wrong);
    }

    public void DecrementWrongCounter()
    {
        wrong--;
        Debug.Log("Wrong Counter: " + wrong);
    }

    public void CheckShapes()
    {
        Renderer LambaRender = lambalight.GetComponent<Renderer>();
        if (correctCounter + wrong == 3)
        {
            if (wrong == 0)
            {
                LambaRender.material = yesilisik;
                lambadoor.tag = "Door";
                kucukbuyuksong.Play();
            }
        }
    }
}
