using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class RaycastForPlayer : MonoBehaviour
{   
    public Animator sceneLoad;

    public GameObject lamba;
    public Material kirmizi;
    public Material yesilisik;

    public Camera cam;
    public Animator anim;
    public Animator dooranim;
    public GameObject doorText;
    public Button_Controller[] buttonController = new Button_Controller[4];
    public Shape_Controller shapeController;
    public GameObject[] Led_Panels = new GameObject[4];

    public GameObject[] Lights = new GameObject[8];
    public GameObject[] Lamps = new GameObject[8];
    public string dooropenanim = "open";
    public string doorcloseanim = "close";

    public Animator donenkarakter;
    public AudioSource lightaudio;
    public AudioClip lightOpen;

    public GameObject panelkapisi;
    public AudioSource tebriklerpanel;
    public AudioSource ilksecimsong;
    public AudioSource fourpanelsong;
    public AudioSource soldansong;
    public AudioSource simdurdur;
    public AudioSource siradakioda;
    public AudioSource tupeatmasong;
    public AudioSource olmamaliydisong;
    public AudioSource siralamasong;
    public AudioSource kandirdimsong;
    public AudioSource armutsong;
    public AudioSource elmasong;
    public AudioSource samuraisong;
    public AudioSource shortFlic;
    public AudioSource longFlic;
    public GameObject samuramaterial;
    public VideoPlayer videoPlayer;
    public AudioSource arkaplansesi;

    private float rotationSpeed = 1.0f;

    private bool isOpening = false;
    private Rigidbody selectedObject;
    private float initialDistance;
    public bool isDragging = false;

    void Awake()
    {
        for (int i = 0; i < 8; i++)
        {
            Material material = Lamps[i].GetComponent<Renderer>().material;
            material.DisableKeyword("_EMISSION");
        }
        
    }

    void Start()
    {
        doorText.SetActive(false);
        
        
    }
    void Update()
    {
        RaycastHit hit;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, 5f))
            {
                if (hit.rigidbody != null)
                {
                    selectedObject = hit.rigidbody;
                    initialDistance = Vector3.Distance(cam.transform.position, selectedObject.transform.position);
                    isDragging = true;
                }
                if (hit.collider.gameObject.tag == "Button")
                {
                    GameObject buttons = hit.collider.gameObject.transform.gameObject;
                    anim = buttons.GetComponent<Animator>();
                    switch(hit.collider.gameObject.name)
                    {
                        case "Mavi_Buton":
                            anim.SetTrigger("button");
                            buttonController[0].ChangeMaterialColor(Led_Panels[0], 0);
                            break;
                        case "Kirmizi_Buton":
                            anim.SetTrigger("button");
                            buttonController[1].ChangeMaterialColor(Led_Panels[1], 1);
                            break;
                        case "Yesil_Buton":
                            anim.SetTrigger("button");
                            buttonController[2].ChangeMaterialColor(Led_Panels[2], 2);
                            break;
                        case "Sari_Buton":
                            anim.SetTrigger("button");
                            buttonController[3].ChangeMaterialColor(Led_Panels[3], 3);
                            break;
                    }

                    Renderer LambaRenderer = lamba.GetComponent<Renderer>();
                    for (int i = 0; i < 4; i++)
                    {
                        if (buttonController[i].correctColorCheck == 0)
                        {
                            break;
                        }
                        if (i == 3)
                        {
                            Debug.Log("Kapi acildi");
                            LambaRenderer.material = yesilisik;
                            tebriklerpanel.Play();
                            panelkapisi.tag = "Door";
                        }
                    }
                }
            }
        }

        if (Physics.Raycast(ray, out hit, 3f))
        {
            

            if (hit.collider.gameObject.tag == "Door" && !isOpening)
            {
                doorText.SetActive(true);
                GameObject doorparent = hit.collider.gameObject.transform.parent.gameObject;
                Animator dooranim = doorparent.GetComponent<Animator>();
                if (Input.GetKeyDown(KeyCode.E))
                {  
                    bool isOpen = dooranim.GetBool("isOpen");

                    if (isOpen)
                    {
                        dooranim.SetBool("isOpen", false);
                        dooranim.SetTrigger("close");
                    }
                    else
                    {
                        dooranim.SetBool("isOpen", true);
                        dooranim.SetTrigger("open");
                    }
                }
            }
            if (hit.collider.gameObject.tag == "yariacik")
            {
                doorText.SetActive(true);
                GameObject doorparent = hit.collider.gameObject.transform.parent.gameObject;
                Animator dooranim = doorparent.GetComponent<Animator>();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    donenkarakter.SetTrigger("rio");
                    bool isOpen = dooranim.GetBool("halfdoor");
                    dooranim.SetBool("halfdoor", true);
                    StartCoroutine(halfdoorclose());

                }
            }
            if (hit.collider.gameObject.tag == "armut")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    armutsong.Play();
                }
            }
            if (hit.collider.gameObject.tag == "elma")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    elmasong.Play();
                }
            }
        }
        else
        {
            doorText.SetActive(false);
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            selectedObject.transform.position = selectedObject.transform.position;
            selectedObject = null;
        }

        if (isDragging && selectedObject != null)
        {
            Vector3 mousePos = GetMouseAsWorldPoint(initialDistance);
            selectedObject.transform.position = mousePos;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "light")
        {
            
            StartCoroutine(openlights());
        }
        if (other.gameObject.tag == "ilksecim")
        {
            ilksecimsong.Play();
        }
        if (other.gameObject.tag == "4panel")
        {
            fourpanelsong.Play();
        }
        if (other.gameObject.tag == "soldangitmenisöyledim")
        {
            soldansong.Play();
        }
        if (other.gameObject.tag == "simulasyonudurdur")
        {
            simdurdur.Play();
        }
        if (other.gameObject.tag == "siradakioda")
        {
            siradakioda.Play();
        }
        if (other.gameObject.tag == "sekilleritupeatma")
        {
            tupeatmasong.Play();
        }
        if (other.gameObject.tag == "olmamaliydi")
        {
            olmamaliydisong.Play();
        }
        if (other.gameObject.tag == "siralamaoyun")
        {
            siralamasong.Play();
        }
        if (other.gameObject.tag == "kandirdim")
        {
            kandirdimsong.Play();
        }
        if (other.gameObject.tag == "samurai")
        {
            
            StartCoroutine(SamuraiLevel());
            
            videoPlayer.Play();
            samuraisong.Play();
            arkaplansesi.Stop();
        }
        if (other.gameObject.tag == "bvbitis")
        {
            StartCoroutine(LoadLevel());
        }
    }

    IEnumerator LoadLevel()
    {
        sceneLoad.SetTrigger("next");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator SamuraiLevel()
    {
        yield return new WaitForSeconds(11.5f);
        Material material = samuramaterial.GetComponent<Renderer>().material;
        material.EnableKeyword("_EMISSION");
        shortFlic.Play();
        yield return new WaitForSeconds(0.05f);
        material.DisableKeyword("_EMISSION");
        yield return new WaitForSeconds(0.1f);
        material.EnableKeyword("_EMISSION");
        longFlic.Play();
        yield return new WaitForSeconds(0.2f);
        material.DisableKeyword("_EMISSION");
        yield return new WaitForSeconds(0.1f);
        material.EnableKeyword("_EMISSION");
        shortFlic.Play();
        
    }
    
    IEnumerator openlights()
    {
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < 8; i++)
        {
            lightaudio.PlayOneShot(lightOpen, 3.14f);
            Material material = Lamps[i].GetComponent<Renderer>().material;
            material.EnableKeyword("_EMISSION");
            Lights[i].SetActive(true);
            yield return new WaitForSeconds(0.1f);
            material.DisableKeyword("_EMISSION");
            Lights[i].SetActive(false);
            yield return new WaitForSeconds(0.1f);
            material.EnableKeyword("_EMISSION");
            Lights[i].SetActive(true);
            yield return new WaitForSeconds(0.3f);
            if (lightaudio.volume > 0.008f)
            {
                lightaudio.volume -= 0.010f;
            }
        }
    }
    IEnumerator halfdoorclose()
    {
        yield return new WaitForSeconds(3.4f);
        dooranim.SetBool("halfdoor", false);
    }
    private Vector3 GetMouseAsWorldPoint(float distance)
    {
        Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
        return mouseRay.GetPoint(distance);
    }
}
