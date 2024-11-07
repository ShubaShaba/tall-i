using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Video : MonoBehaviour
{

     [SerializeField] VideoPlayer video;

    void Start()
    {
        video.loopPointReached += doSomethingWhenVideoFinish;
    }

 


     void doSomethingWhenVideoFinish(VideoPlayer vp)
    {
        SceneManager.LoadScene("Level1");
    }


}