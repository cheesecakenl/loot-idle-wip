using System.Collections.Generic;
using UnityEngine;

namespace Dev4All.CodeSnippets.LootIdle
{
    [System.Serializable]
    public class AudioData
    {
        public AudioType audioType;
        public string label;
        public List<AudioClip> clips;
    }
}