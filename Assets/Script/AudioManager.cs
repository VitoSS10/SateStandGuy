using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    bool alreadyAdd;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        
        foreach(Sound sound in sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.clip;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;
            sound.audioSource.loop = sound.loop;
        }
    }   

    public Sound[] sounds;

    private void Start()
    {
       
    }

    public void Play(string nameAudio)
    {
        Sound sound = Array.Find(sounds, sounds => sounds.name == nameAudio);
        
        if(sound == null) 
        {
            Debug.Log("Error! " + nameAudio + "Not found!");
            return;
        }
        
        sound.audioSource.Play();
    }  
}