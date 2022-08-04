using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    [SerializeField] private AudioSource musicSource, effectSource;
    public Sound[] sounds;
    public Sound[] tracks;

    private string[] jumpSounds = {"jump01","jump02","jump03"};
    private string[] grappleSounds = {"grapple01","grapple02","grapple03"};
    private string[] hitSounds = {"hit01","hit02","hit03","hit04"};

    private float musicFadeTime = 5f;
    private float musicStartTime;
    private float musicFadeFactor;
    private float musicVolume = 0.66f;
    private bool musicFading = false;

    // AWAKE
    private void Awake()
    {
        // MAKE SINGLETON
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }

        // ZERO MUSIC VOLUME
        musicSource.volume = 0f;
    }

    // START
    private void Start()
    {
        PlayMusic("OneButtonBeats");        
    }

    // FIXED UPDATE
    private void FixedUpdate()
    {
        if (musicFading) MusicFadeIn();
    }

    // PLAY SOUND EFFECT
    public void Play(string name)
    {
        if (name == "jump") { name = jumpSounds[UnityEngine.Random.Range(0, jumpSounds.Length)]; }
        if (name == "grapple") { name = grappleSounds[UnityEngine.Random.Range(0, grappleSounds.Length)]; }
        if (name == "hit") { name = hitSounds[UnityEngine.Random.Range(0, hitSounds.Length)]; }

        Sound s = Array.Find(sounds, sound => sound.name == name);
        effectSource.PlayOneShot(s.clip);
    }

    // PLAY MUSIC TRACK
    public void PlayMusic(string name)
    {
        musicStartTime = Time.time;
        musicFading = true;

        Sound s = Array.Find(tracks, sound => sound.name == name);
        musicSource.clip = s.clip;

        musicSource.Play();
    }

    // MUSIC FADE IN
    private void MusicFadeIn()
    {
        musicFadeFactor = Globals.EaseInSine(Mathf.Min(1f, ((Time.time - musicStartTime) / musicFadeTime)), 0f, 1f, 1f);
        if (musicFadeFactor >= 1f) musicFading = false;

        musicSource.volume = musicVolume * musicFadeFactor;
    }
}
