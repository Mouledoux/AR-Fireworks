using UnityEngine;
using System.Collections.Generic;

/// <Description>
/// 
/// </Description>

public class FireworkManager : MonoBehaviour
{
    #region Variables
    [Header("Set Up Variables")]
    [SerializeField] private GameObject fireworkPrefab;
    [Range(10, 100)] [SerializeField] private int numOfLaunchers = 35;
    [Range(5,90)]    [SerializeField] private float radius = 20f;
    //private AudioSource audio;
    private GameObject[] launchers;
    #endregion

    #region Functions
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    // MonoBehaviour
    void Awake ()
    {
        //audio = GetComponent<AudioSource>();
        SetUpFireworks();
	}
	
	void Update ()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            Application.Quit();

        UpdateViaAudio();
	}
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    // Custom Functions
    [ContextMenu("Set Up Fireworks")]
    private void SetUpFireworks()
    {
        for (int i = 0; i < numOfLaunchers; i++)
        {
            if (i >= numOfLaunchers/2)
            {
                float angle = i * Mathf.PI / numOfLaunchers;
                Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
                Instantiate(fireworkPrefab, pos, Quaternion.identity);
            }
            else
            {
                float angle = i * Mathf.PI / numOfLaunchers;
                Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
                Instantiate(fireworkPrefab, pos, Quaternion.identity);
            }
        }
        launchers = GameObject.FindGameObjectsWithTag("FireWork");
        foreach(GameObject g in launchers)
        {
            g.transform.parent = transform;
        }
    }

    private void UpdateViaAudio()
    {
        float[] spectrum = AudioListener.GetSpectrumData(1024, 0, FFTWindow.Hamming);

        int i = 0;
        int t = 0;
        int s = numOfLaunchers/2;
        foreach (GameObject g in launchers)
        {
            Vector3 previousScale = g.transform.localScale;
            if (i <= numOfLaunchers /2)
            {
                previousScale.y = spectrum[numOfLaunchers / 2 - t] * 20;
                g.transform.localScale = previousScale;
                g.transform.position = new Vector3(g.transform.position.x, 0, g.transform.position.z);
                g.transform.position += new Vector3(0, previousScale.y, 0);
                t++;
            }
            else
            {
                previousScale.y = spectrum[numOfLaunchers / 2 - s] * 20;
                g.transform.localScale = previousScale;
                g.transform.position = new Vector3(g.transform.position.x, 0, g.transform.position.z);
                g.transform.position += new Vector3(0, previousScale.y, 0);
                s--;
            }

            i++;
        }
    }
    #endregion
}
