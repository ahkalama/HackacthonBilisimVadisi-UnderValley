using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    public float walkspeed = 5.0f;
    public float runspeed = 10.0f;
    public float jumpspeed = 8.0f;
    public float gravity = 9.8f;

    public float frequency = 1.5f;
    public float height = 0.5f;
    public float swayangle = 5.0f;
    public float mousesens = 2.0f;

    public Camera playercamera;
    public Vector2 mouse;

    private CharacterController controller;
    private Vector3 movedirection = Vector3.zero;
    private float defaultYpos = 0;
    private float timer = 0;
    private float rotationX = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            Debug.LogError("CharacterController component is missing from this game object.");
            return;
        }

        if (playercamera == null)
        {
            playercamera = Camera.main;
            if (playercamera == null)
            {
                Debug.LogError("Main Camera is missing from the scene.");
                return;
            }
        }

        defaultYpos = playercamera.transform.localPosition.y;
    }

    void Update()
    {
        // mouse

        mouse.x += Input.GetAxis("Mouse X") * mousesens;
        mouse.y += Input.GetAxis("Mouse Y") * mousesens;

        mouse.y = Mathf.Clamp(mouse.y, -60, 60);

        transform.localRotation = Quaternion.Euler(-mouse.y, mouse.x, 0);

        // movement

        float speed = Input.GetKey(KeyCode.LeftShift) ? runspeed : walkspeed;
        float movedirectionY = movedirection.y;
        movedirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        movedirection = transform.TransformDirection(movedirection);
        movedirection *= speed;

        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                movedirectionY = jumpspeed;
            }
        }

        movedirection.y = movedirectionY - (gravity * Time.deltaTime);
        controller.Move(movedirection * Time.deltaTime);

        HeadBobbing();
    }

    void HeadBobbing()
    {
        if (Mathf.Abs(movedirection.x) > 0.1f || Mathf.Abs(movedirection.z) > 0.1f)
        {
            timer += Time.deltaTime * frequency;
            playercamera.transform.localRotation = Quaternion.Euler(1, playercamera.transform.localRotation.y, playercamera.transform.localRotation.z);
        }
        else
        {
            timer = 0;
            playercamera.transform.localRotation = Quaternion.Euler(0, playercamera.transform.localRotation.y, playercamera.transform.localRotation.z);
        }
    }
}
