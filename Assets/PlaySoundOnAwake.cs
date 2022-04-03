using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnAwake : MonoBehaviour
{
    public string soundName;
    void Awake()
    {
        SoundManager.instance.Play(soundName);
    }
}
