using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static double audioTime;
    private static double prevTime;
    private static double addTime;
    public static double AudioTime
    {
        get
        {
            return AudioSettings.dspTime + addTime - audioTime;
        }
    }

    private AudioSource audioSource;

    void Awake()
    {
        audioTime = AudioSettings.dspTime + 2;
        prevTime = AudioSettings.dspTime;
        addTime = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayScheduled(audioTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (AudioSettings.dspTime == prevTime)
            addTime += Time.unscaledDeltaTime;
        else
            addTime = 0;
        prevTime = AudioSettings.dspTime;
    }
}
