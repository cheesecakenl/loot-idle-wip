using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Dev4All.CodeSnippets.LootIdle
{
    [CreateAssetMenu(menuName = "Dev4All/CodeSnippets/LootIdle/Create Audio Database...")]
    public class AudioDatabase : ScriptableObject
    {
        [SerializeField]
        public AudioMixer audioMixer;

        [SerializeField]
        public List<AudioData> entries;

        public AudioClip GetAudioClip(AudioType audioType, string label)
        {
            foreach (AudioData entry in entries)
            {
                if (entry.audioType == audioType && entry.label == label && entry.clips.Count > 0)
                {
                    int idx = Random.Range(0, entry.clips.Count);

                    return entry.clips[idx];
                }
            }

            return null;
        }
    }
}