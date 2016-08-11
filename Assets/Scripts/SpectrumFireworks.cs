using UnityEngine;
using System.Collections.Generic;



public class SpectrumFireworks : MonoBehaviour
{
    private string[] FWs = {"Firework_01_8x8-64",
                            "Firework_02_8x8-64",
                            "Firework_03_8x8-64",
                            "Firework_04_8x8-64",
                            "Firework_05_8x8-64",
                            "Firework_06_8x8-64",
                            "Firework_07_8x8-64",
                            "Firework_08_8x8-64",
                            "Firework_09_8x8-64",
                            "Firework_10_8x8-64"};
    private ParticleSystem fireWorks;
    private GameObject death;

    [SerializeField] private AudioClip onDeathAudio;
    [SerializeField] private AudioClip onBirthAudio;

    AudioSource fireworkSounds;

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    // MonoBehaviour
    void Awake()
    {
        fireWorks       = gameObject.GetComponent<ParticleSystem>();
        death           = transform.FindChild("Death").gameObject;
        fireworkSounds  = gameObject.GetComponent<AudioSource>();
    }
	
	void Update ()
    {
        if (transform.parent.transform.localScale.y > 2f && !fireWorks.isPlaying)
        {
            if (fireWorks.particleCount <= 0)
            {
                int i = Random.Range(1, 11);
                death.GetComponent<Renderer>().material = Resources.Load(FWs[i], typeof(Material)) as Material;
            }
            fireWorks.Play();
            fireworkSounds.PlayOneShot(onBirthAudio, 0.02f);
        }

        if (fireWorks.particleCount == 0 && fireWorks.isPlaying)
        {
            fireworkSounds.PlayOneShot(onDeathAudio, Random.RandomRange(0.4f, 0.8f));
        }

        else
        {
            fireWorks.Stop();
        }
	}
}
