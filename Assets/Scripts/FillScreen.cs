using UnityEngine;
using System.Collections;

public class FillScreen : MonoBehaviour
{
    private Camera cam;

    void Start()
    {
        cam = transform.parent.GetComponent<Camera>();    
    }

    void Update()
    {
            float pos = (45);

            float h = Mathf.Tan(cam.fieldOfView * Mathf.Deg2Rad * 0.5f) * pos * 2f;

            transform.localScale = new Vector3(1.5f * h * cam.aspect, 2f * h, 1f);
        
    }
}
