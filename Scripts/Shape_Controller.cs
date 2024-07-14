using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shape_Controller : MonoBehaviour
{
    public static Shape_Controller Instance;
    public Material yesilisik;

    public GameObject lambadoor;
    public GameObject lambalight;
    public AudioSource tebriklerpanel2;
    public AudioSource tebriklerpanel1;

    public int shapeCounter = 0;
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

    public void IncrementShapeCounter()
    {
        shapeCounter++;
    }

    public void IncrementWrongCounter()
    {
        wrong++;
    }

    public void IncrementStepCounter()
    {
        stepCounter++;
    }

    public void CheckShapes()
    {
        Renderer LambaRender = lambalight.GetComponent<Renderer>();
        if (shapeCounter + wrong == 3)
        {
            if (wrong == 0)
            {
                Debug.Log("Win");
                LambaRender.material = yesilisik;
                tebriklerpanel2.Play();
                lambadoor.tag = "Door";
            }
            else
            {
                Debug.Log("Lose");
                LambaRender.material = yesilisik;
                tebriklerpanel1.Play();
                lambadoor.tag = "Door";
            }
        }
    }
}
