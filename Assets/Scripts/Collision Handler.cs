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

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;
    AudioSource audioSource; //nhập nguồn âm thanh

    bool isTransitioning = false;
    bool collisionDisabled = false; // vô hiệu hóa va chạm 
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        RepondToDebugKeys();
    }

    void RepondToDebugKeys()
    {
        if(Input.GetKeyDown(KeyCode.L)){
            LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = ! collisionDisabled; // chuyển đổi vô hiệu hóa va chạm hoặc ngược lại
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || collisionDisabled )// nếu đang chuyển dổi thì quay lại
        {
            return;
        }
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
        //khong lap lai audio 2 lan
        isTransitioning = true;
        audioSource.Stop(); // đừng audio
        audioSource.PlayOneShot(success); //phat audio khi success
        successParticles.Play();// tạo hiệu ứng khi success
        GetComponent<Movement>().enabled = false;//vô hiệu hóa các tác động khi qua màn 
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void StartCrashSequence()
    {
        //khong lap lai audio 2 lan
        isTransitioning = true;
        audioSource.Stop(); // đừng audio
        audioSource.PlayOneShot(crash); //phat audio khi crash
        crashParticles.Play();// tạo hiệu ứng khi success
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
