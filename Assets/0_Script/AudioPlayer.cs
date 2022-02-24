using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioSource _source;
    [SerializeField]
    private List<StringAudioPair> _clipList;

    private Dictionary<string, AudioClip> _clipDict;

    public void Awake()
    {
        _clipDict = new Dictionary<string, AudioClip>();

        foreach(StringAudioPair pair in _clipList)
        {
            _clipDict.Add(pair.key, pair.clip);
        }
    }

    public void TryPlayClip(string audioKey)
    {
        // kolla avstånd mellan _source.transform och spelaren

        PlayClip(audioKey);
    }

    public void PlayClip(string audioKey)
    {
        _source.PlayOneShot(_clipDict[audioKey]);
    }

    public void LoopClip(string audioKey)
    {
        _source.clip = _clipDict[audioKey];
        _source.loop = true;
        _source.Play();
    }

    public void StopAudio()
    {
        _source.Stop();
    }
}
