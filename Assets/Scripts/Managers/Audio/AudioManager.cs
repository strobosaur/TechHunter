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

    private string[] shootSounds = {"pistol01","pistol02","pistol03"};
    private string[] meleeSounds = {"melee01","melee02","melee03"};
    private string[] menuSounds = {"menu_choice","menu_blip"};
    private string[] hitSounds = {"hit01","hit02","hit03","hit04"};

    private float musicFadeTime = 7.5f;
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
        PlayMusic("MenuOST");
    }

    // FIXED UPDATE
    private void FixedUpdate()
    {
        if (musicFading) MusicFadeIn();
    }

    // PLAY SOUND EFFECT
    public void Play(string name)
    {
        if (name == "menu_blip") { name = menuSounds[1]; }
        if (name == "menu_choice") { name = menuSounds[0]; }
        if (name == "shoot") { name = shootSounds[UnityEngine.Random.Range(0, shootSounds.Length)]; }
        if (name == "melee") { name = meleeSounds[UnityEngine.Random.Range(0, meleeSounds.Length)]; }
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
