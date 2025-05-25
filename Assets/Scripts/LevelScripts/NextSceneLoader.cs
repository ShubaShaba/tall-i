using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private float timeToLoad;

    private void DelayedLoad() { SceneManager.LoadScene(sceneName); }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            Invoke(nameof(DelayedLoad), timeToLoad);
    }
}
