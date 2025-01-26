using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public static Dictionary<string, AudioClip> AudioDB;

    [SerializeField] private NewDict AudioDBSet;

    [SerializeField] private AudioSource soundObject;

    private void Awake()
    {
        if (AudioDBSet != null)
        {
            AudioDB = AudioDBSet.ToDictionary();
        }

        instance = this;
    }

    public void PlaySound(string name, Vector3 pos, float volume = 1f)
    {
        if (AudioDB.ContainsKey(name))
        {
            float pitch = UnityEngine.Random.Range(0.9f, 1.1f);
            AudioSource audioSource = Instantiate(soundObject, pos, Quaternion.identity);
            audioSource.clip = AudioDB[name];
            audioSource.volume = volume;
            audioSource.pitch = pitch;
            audioSource.Play();
            Destroy(audioSource, audioSource.clip.length);
            Debug.Log(name);
        }
    }

}

[Serializable]

class NewDict
{
    [SerializeField] private NewDictItem[] soundDB;

    public Dictionary<string, AudioClip> ToDictionary()
    {
        Dictionary<string, AudioClip> newDict = new Dictionary<string, AudioClip>();

        foreach (var sound in soundDB)
        {
            newDict.Add(sound.name, sound.clip);
        }

        return newDict;
    }
}


[Serializable]
class NewDictItem
{
    [SerializeField] public string name;
    [SerializeField] public AudioClip clip;
}