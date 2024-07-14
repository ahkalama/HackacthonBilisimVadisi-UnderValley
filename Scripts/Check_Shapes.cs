using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_Shapes : MonoBehaviour
{
    public string correctTag;
    private RaycastForPlayer raycastForPlayer;

    void Start()
    {
        raycastForPlayer = FindObjectOfType<RaycastForPlayer>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!raycastForPlayer.isDragging)
        {
            if (other.gameObject.tag == correctTag)
            {
                Shape_Controller.Instance.IncrementShapeCounter();
                this.gameObject.SetActive(false);
            }
            else
            {
                Shape_Controller.Instance.IncrementWrongCounter();
                this.gameObject.SetActive(false);
            }
            Shape_Controller.Instance.IncrementStepCounter();
            Shape_Controller.Instance.CheckShapes();
        }
    }
}
