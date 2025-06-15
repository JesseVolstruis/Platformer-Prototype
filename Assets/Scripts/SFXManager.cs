using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
   public static SFXManager instance;
    [SerializeField]
    private AudioSource sfxObject;
    private Dictionary<AudioClip, float> lastPlayedTime = new Dictionary<AudioClip, float>();
    [SerializeField] private float clipCooldown = 0.1f; 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    public void PlayClip(AudioClip clip, Transform pos, float volume)
    {
        AudioSource audioSource = Instantiate(sfxObject, pos.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.pitch = Random.Range(1f, 1.4f);
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlaySingleClip(AudioClip clip, Transform pos, float volume)
    {
        float currentTime = Time.time;

   
        if (lastPlayedTime.TryGetValue(clip, out float lastTime))
        {
            if (currentTime - lastTime < clipCooldown)
                return; 
        }
        lastPlayedTime[clip] = currentTime;
        AudioSource audioSource = Instantiate(sfxObject, pos.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.pitch = Random.Range(1f, 1.4f);
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }
}
