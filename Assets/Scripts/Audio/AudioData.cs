using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioData
{
    public AudioType audioType;
    public string label;
    public List<AudioClip> clips;
}