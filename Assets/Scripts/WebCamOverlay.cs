using UnityEngine;
using System.Collections;

public class WebCamOverlay : MonoBehaviour 
{
    // Assign the Material you are using for the web cam feed
    [SerializeField] private Material webCamTex;  

	void Start()
	{
        // Grabbing all web cam devices
        WebCamDevice[] devices = WebCamTexture.devices;

        // I just use the first one, use which ever one you need 
        string camName = devices[0].name;

        // set the Texture from the cam feed
        WebCamTexture camFeed = new WebCamTexture (camName);

        // Assign the materials texture to the WebCamTexture you made,
        // this applies it to all objects using this Material
        webCamTex.mainTexture = camFeed;

        // Then start the texture
        camFeed.Play ();    
	}

    [ContextMenu("Find WebCams")]
    public void FindWebcam()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        for (int i = 0; i < devices.Length; i++)
            Debug.Log(devices[i].name);
        if(devices.Length == 0)
            Debug.Log("No Web Camera Detected");
    }
}
