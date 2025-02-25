using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    AudioSource audioSource; //nhập nguồn âm thanh

    void Start()
    {
        audioSource =GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            case "Fuel":
                Debug.Log("Fuel");
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence()
    {
        audioSource.PlayOneShot(success); //phat audio khi success
        GetComponent<Movement>().enabled = false;//vô hiệu hóa các tác động khi qua màn 
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void StartCrashSequence()
    {   
        audioSource.PlayOneShot(crash); //phat audio khi crash
        //vô hiệu hóa các tác động khi qua màn 
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }
    void LoadNextLevel() //Chuyển Scence tiếp theo 
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; //cảnh hiện tại 
        int nextSceneIndex = currentSceneIndex + 1;                       //cảnh tiếp theo
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel() // tải lại cảnh 
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
