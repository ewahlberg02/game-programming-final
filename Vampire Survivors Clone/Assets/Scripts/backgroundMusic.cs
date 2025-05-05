using UnityEngine;

public class backgroundMusic : MonoBehaviour
{
    private AudioSource backgroundAudio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        backgroundAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0){
            backgroundAudio.Pause();
        } else {
            backgroundAudio.UnPause();
        }
    }
}
