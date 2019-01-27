using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip defaultSpaceTheme;
    public float fadeInTime = 3f;

    private AudioSource source;
    private AudioSource crossFade;
    private bool onMainSource;

    void Start()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        onMainSource = true;
        source = GetComponent<AudioSource>();
        crossFade = transform.GetChild(0).gameObject.GetComponent<AudioSource>();
    }
    public void SetMusic()
    {
        StartCoroutine(FadeIn(defaultSpaceTheme));
    }
    public void SetMusic(AudioClip newClip)
    {
        StartCoroutine(FadeIn(newClip));
    }

    IEnumerator FadeIn(AudioClip newClip)
    {
        if(onMainSource)
        {
            crossFade.clip = newClip;
            crossFade.Play();
            crossFade.time = Time.time;
        }
        else
        {
            source.clip = newClip;
            source.Play();
            source.time = Time.time;
        }
        float timer = 0f;
        while(timer < fadeInTime)
        {
            timer += Time.deltaTime;
            float ratio = timer / fadeInTime;
            if(onMainSource)
            {
                source.volume = 1 - ratio;
                crossFade.volume = ratio;
            }
            else
            {
                source.volume = ratio;
                crossFade.volume = 1 - ratio;
            }

            yield return null;
        }
        onMainSource = !onMainSource;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
