using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    [SerializeField] SoundFXManager soundPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

   void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            soundPlayer.Thud1Sound();  // Play the first sound when Space is pressed
        }

    }
}
