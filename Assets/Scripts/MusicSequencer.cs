using System;
using UnityEngine;

public class MusicSequencer : MonoBehaviour
{
    public static MusicSequencer Instance;
    public event Action<double> PromptNextAudioToPlay;
   
    [SerializeField]
    private float bpm = 120.0f;
    [SerializeField]
    private float numBeatsPerSegment = 16.0f;

    private double nextEventTime;
    private bool running = false;
 
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        nextEventTime = AudioSettings.dspTime + 2.0f;
        running = true;
    }

    private void Update()
    {
        if(!running)
        {
            return;
        }

        double time = AudioSettings.dspTime;

        if(time + 1.0f > nextEventTime)
        {
            PromptNextAudioToPlay?.Invoke(nextEventTime);
            nextEventTime += 60.0f / (bpm * numBeatsPerSegment);
        }
    }
}
