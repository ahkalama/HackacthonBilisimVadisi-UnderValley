using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Controller : MonoBehaviour
{
    private int colorCounter = 0;
    public int correctColorCheck = 0;
    private Color[] colors = new Color[4];
    // Start is called before the first frame update
    void Start()
    {
        colors = new Color[4] {Color.blue, Color.red, Color.green, Color.yellow};
    }


    public void ChangeMaterialColor(GameObject obj, int correctColor)
    {
        obj.GetComponent<Renderer>().material.color = colors[colorCounter];
        if (colorCounter == correctColor)
        {
            correctColorCheck = 1;
        }
        else
        {
            correctColorCheck = 0;
        }
        colorCounter++;
        if (colorCounter == 4)
        {
            colorCounter = 0;
        }
    }
}
