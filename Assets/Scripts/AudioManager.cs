using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Random = System.Random;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    [SerializeField]
    private List<GameObject> audioSources;

    [SerializeField]
    private AudioMixerSnapshot firstSnapShot;
    [SerializeField]
    private List<AudioMixerSnapshot> snapshots;
    
    private int audioSourceIndex;
    private int snapshotIndex = 0;

    private static Random rng = new Random();

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        firstSnapShot.TransitionTo(0.5f);
    }

    public GameObject GetNextAudioSourceGameObject()
    {
        return audioSources[audioSourceIndex++ % audioSources.Count];
    }

    public void SetAudioMixerChannel()
    {
        if(snapshotIndex != 0 && snapshotIndex % snapshots.Count == 0)
        {
            snapshots[snapshotIndex % snapshots.Count].TransitionTo(1.5f);
        }
        else
        {
            snapshots[snapshotIndex % snapshots.Count].TransitionTo(0.5f);
        }
        
        snapshotIndex++;
    }

    public void ResetAudioSources()
    {
        foreach(GameObject audioSourceObject in audioSources)
        {
            audioSourceObject.transform.parent = transform;
        }
    }
}
