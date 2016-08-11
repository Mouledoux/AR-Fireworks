using UnityEngine;
using System.Collections;

public class CanvasVrSetup : MonoBehaviour
{
    Camera cameraLeft;
    Camera cameraRight;

    [Space(10)]
    [Header("AR Cam Settings")]
    private string camName;
    private WebCamTexture camFeed;
    public Material webCamTex;
    public GameObject quadPrefab;
    //
    public Canvas leftCanvas;
    public Canvas rightCanvas;
    //
    void Start ()
    {
        StartCoroutine("FindandSetCanvas");
        SetUpWebCam();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            gameObject.GetComponent<GvrViewer>().VRModeEnabled = !gameObject.GetComponent<GvrViewer>().VRModeEnabled;
        }
    }
	
    IEnumerator FindandSetCanvas()
    {
        while(cameraLeft == null && cameraRight == null)
        {
            if(GameObject.Find("Main Camera Left") && GameObject.Find("Main Camera Right"))
            {
                cameraLeft = GameObject.Find("Main Camera Left").GetComponent<Camera>();
                cameraRight = GameObject.Find("Main Camera Right").GetComponent<Camera>();
            }
            yield return null;

        }

        leftCanvas.worldCamera = cameraLeft;
        rightCanvas.worldCamera = cameraRight;

        //GameObject le = Instantiate(quadPrefab);
        //GameObject re = Instantiate(quadPrefab);
        //le.name = "LeftEyeFillQuad";
        //re.name = "RightEyeFillQuad";
        //le.transform.parent = cameraLeft.transform;
        //re.transform.parent = cameraRight.transform;
        //le.layer = 8;
        //re.layer = 9;

        //cameraRight.cullingMask = 0 << -1 &  1 << LayerMask.NameToLayer("Defualt") & 1 << LayerMask.NameToLayer("LeftProjection");
        //cameraLeft.cullingMask  &= ~(0 << LayerMask.NameToLayer("RightProjection"));



    }

    void SetUpWebCam()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        camName = devices[0].name;
        camFeed = new WebCamTexture(camName);
        webCamTex.mainTexture = camFeed;
        camFeed.Play();
    }

    [ContextMenu("Find WebCams")]
    public void FindWebcam()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        for (int i = 0; i < devices.Length; i++)
            Debug.Log(devices[i].name);
        if (devices.Length == 0)
            Debug.Log("No Web Camera Detected");
    }
}
