using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Numeric_Cubes : MonoBehaviour
{
    public string correctTag;
    private RaycastForPlayer raycastForPlayer;

    void Start()
    {
        raycastForPlayer = FindObjectOfType<RaycastForPlayer>();

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == correctTag)
        {
            Number_Controller.Instance.IncrementCorrectCounter();
        }
        else
        {
            Number_Controller.Instance.IncrementWrongCounter();
        }
        Number_Controller.Instance.CheckShapes();
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (raycastForPlayer.isDragging)
        {
            if (other.gameObject.tag == correctTag)
            {
                Number_Controller.Instance.DecrementCorrectCounter();
            }
            else
            {
                Number_Controller.Instance.DecrementWrongCounter();
            }
            Number_Controller.Instance.CheckShapes();
        }
    }
}
