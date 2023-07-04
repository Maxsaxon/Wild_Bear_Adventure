using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearSoundFX : MonoBehaviour
{
    private AudioSource soundFX;
    [SerializeField] private AudioClip move_sound, attack_sound_1, attack_sound_2, die_sound;

   
    void Awake()
    {
        soundFX = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void Attack_1()
    {
        soundFX.clip = attack_sound_1;
        soundFX.Play();
    }

     public void Attack_2()
    {
        soundFX.clip = attack_sound_2;
        soundFX.Play();
    }

    public void Die()
    {
        soundFX.clip = die_sound;
        soundFX.Play();
    }
}

