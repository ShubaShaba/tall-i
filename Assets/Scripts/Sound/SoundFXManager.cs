using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public AudioClip Time_Stop_Clip;
    public AudioClip Constant_Time_Stop_Clip; 
    public AudioClip Time_Rewind_Clip; 
    public AudioClip Manual_Rewind_Clip;  
    public AudioClip HardReset_Clip; 
    public AudioClip Heavy_Metal_Impact_Clip; 
    public AudioClip Metal_Clang_Clip; 
    public AudioClip Thud1_Clip; 
    public AudioClip Thud2_Clip; 
    public AudioClip Thud3_Clip; 
    public AudioClip Thud4_Clip;
    public AudioClip Soft_Thud_Clip;
    public AudioClip Stone_Impact_Clip;
    public AudioClip Ambience_hum_Clip; 
    public AudioClip Ambience2_moody_Clip;
    public AudioClip Ambience3_rumble_Clip;
    public AudioClip Robot_Movement_Clip; 
    public AudioClip Robot_Beep1_Clip; 
    public AudioClip Robot_Beep2_Clip;   
    public AudioClip Robot_Beep3_Clip;   
    public AudioClip Robot_Beep4_Clip; 
    public AudioClip Stone_Scraping_Short_Clip; 
    public AudioClip Stone_Scraping_Clip;
    public AudioClip Generator_Clip;   
    
    private AudioSource audioSource;


    void Start(){
        audioSource = GetComponent<AudioSource>();
    }         
    

    public void TimeStopSound(){
        audioSource.PlayOneShot(Time_Stop_Clip);
    }

    public void TimeRewindSound(){
        audioSource.PlayOneShot(Time_Rewind_Clip);
    }

    public void ConstantTimeRewindSound(){
        audioSource.PlayOneShot(Constant_Time_Stop_Clip);
    }

    public void ManualRewindSound(){
        audioSource.PlayOneShot(Manual_Rewind_Clip);
    }

    public void ResetSound(){
        audioSource.PlayOneShot(HardReset_Clip);
    }

    public void MetalImpactSound(){
        audioSource.PlayOneShot(Heavy_Metal_Impact_Clip);
    }


    public void Thud1Sound(){
        audioSource.PlayOneShot(Thud1_Clip);
    }

    public void Thud2Sound(){
        audioSource.PlayOneShot(Thud2_Clip);
    }

    public void Thud3Sound(){
        audioSource.PlayOneShot(Thud3_Clip);
    }

    public void Thud4Sound(){
        audioSource.PlayOneShot(Thud4_Clip);
    }

    public void SoftThudSound(){
        audioSource.PlayOneShot(Soft_Thud_Clip);
    }

    public void StoneImpactSound(){
        audioSource.PlayOneShot(Stone_Impact_Clip);
    }

    public void AmbienceHumSound(){
        audioSource.PlayOneShot(Ambience_hum_Clip);
    }

    public void Ambience2HumSound(){
        audioSource.PlayOneShot(Ambience2_moody_Clip);
    }

    public void Ambience3HumSound(){
        audioSource.PlayOneShot(Ambience3_rumble_Clip);
    }

    public void RobotMovementSound(){
        audioSource.PlayOneShot(Robot_Movement_Clip);
    }

    public void RobotBeepSound1(){
        audioSource.PlayOneShot(Robot_Beep1_Clip);
    }

    public void RobotBeepSound2(){
        audioSource.PlayOneShot(Robot_Beep2_Clip);
    }

    public void RobotBeepSound3(){
        audioSource.PlayOneShot(Robot_Beep3_Clip);
    }

    public void RobotBeepSound4(){
        audioSource.PlayOneShot(Robot_Beep4_Clip);
    }

    public void StoneScrappingSound(){
        audioSource.PlayOneShot(Stone_Scraping_Clip);
    }

    public void StoneScrappingShortSound(){
        audioSource.PlayOneShot(Stone_Scraping_Short_Clip);
    }

    public void GeneratorSound(){
        audioSource.PlayOneShot(Generator_Clip);
    }









}
