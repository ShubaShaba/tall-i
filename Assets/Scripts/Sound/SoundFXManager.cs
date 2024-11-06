using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    [SerializeField] private AudioClip Time_Stop_Clip;
    [SerializeField] private AudioClip Constant_Time_Stop_Clip; 
    [SerializeField] private AudioClip Time_Rewind_Clip; 
    [SerializeField] private AudioClip Manual_Rewind_Clip;  
    [SerializeField] private AudioClip HardReset_Clip; 
    [SerializeField] private AudioClip Heavy_Metal_Impact_Clip; 
    [SerializeField] private AudioClip Metal_Clang_Clip; 
    [SerializeField] private AudioClip Thud1_Clip; 
    [SerializeField] private AudioClip Thud2_Clip; 
    [SerializeField] private AudioClip Thud3_Clip; 
    [SerializeField] private AudioClip Thud4_Clip;
    [SerializeField] private AudioClip Soft_Thud_Clip;
    [SerializeField] private AudioClip Stone_Impact_Clip;
    [SerializeField] private AudioClip Ambience_hum_Clip; 
    [SerializeField] private AudioClip Ambience2_moody_Clip;
    [SerializeField] private AudioClip Ambience3_rumble_Clip;
    [SerializeField] private AudioClip Robot_Movement_Clip; 
    [SerializeField] private AudioClip Robot_Beep1_Clip; 
    [SerializeField] private AudioClip Robot_Beep2_Clip;   
    [SerializeField] private AudioClip Robot_Beep3_Clip;   
    [SerializeField] private AudioClip Robot_Beep4_Clip; 
    [SerializeField] private AudioClip Stone_Scraping_Short_Clip; 
    [SerializeField] private AudioClip Stone_Scraping_Clip;
    [SerializeField] private AudioClip Generator_Clip;            
    

    public AudioSource audioSource1;
    public AudioSource audioSourceLoop;



    public void PlaySound()
    {
        //assigning audioclip
        audioSource1.clip = audioClip;
        Debug.Log(audioClip);

        //playing clip
        audioSource1.Play();
    }

}
