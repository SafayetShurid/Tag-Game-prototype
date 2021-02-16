using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioSource cautionSound;
    public AudioSource winSound;
    public AudioSource damageSound;
    public AudioSource enteredSafeZone;

    public static SoundManager instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayCautionSound()
    {
        cautionSound.Play();
    }

    public void PlaywinSound()
    {
        winSound.Play();
    }

    public void PlayDamageSound()
    {
        damageSound.Play();
    }

    public void PlayEnterSafeZone()
    {
        enteredSafeZone.Play();
    }
}
