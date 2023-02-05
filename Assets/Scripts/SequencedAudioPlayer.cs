using System.Collections.Generic;
using UnityEngine;

public class SequencedAudioPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioClip audioClip;
    private List<AudioSource> myAudioSources = new();

    private void Awake()
    {
        for(int i = 0; i < 8; i++)
        {
            CreateAndAddAudioSource();
        }
    }

    private void Start()
    {
        MusicSequencer.Instance.PromptNextAudioToPlay += OnNextAudioToPlay;
    }

    private void OnNextAudioToPlay(double nextEventTime)
    {
        // Debug.Log(nextEventTime);
        AudioSource audioSource = GetNextAudioSource();
        audioSource.PlayScheduled(nextEventTime);
    }

    private AudioSource GetNextAudioSource()
    {
        foreach(AudioSource audioSource in myAudioSources)
        {
            if(!audioSource.isPlaying)
            {
                return audioSource;
            }
        }

        return CreateAndAddAudioSource();
    }

    private AudioSource CreateAndAddAudioSource()
    {
        GameObject audioPlayer = new GameObject("audioPlayer");
        audioPlayer.transform.parent = transform;
        audioPlayer.transform.position = transform.position;

        AudioSource initAudioSource = audioPlayer.AddComponent<AudioSource>();
        initAudioSource.clip = audioClip;
        initAudioSource.spatialBlend = 1.0f;
        
        myAudioSources.Add(initAudioSource);
        return initAudioSource;
    }
}
