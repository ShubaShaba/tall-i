using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodySound : MonoBehaviour
{
    private Rigidbody rb;
    SoundFXManager soundManager;
    [SerializeField] AudioClip sound = null;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundFXManager>();
    }

    void OnCollisionEnter()
    {
        soundManager.PlaySound(Sound.Thud4, false, transform, sound);
    }
}
