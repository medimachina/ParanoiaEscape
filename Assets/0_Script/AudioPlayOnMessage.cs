using com.ootii.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayOnMessage : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private List<StringAudioPair> _clipList;

    private Dictionary<string, AudioClip> _clipDict;

    private void Awake()
    {
        _clipDict = new Dictionary<string, AudioClip>();
        foreach (StringAudioPair pair in _clipList)
        {
            _clipDict.Add(pair.key, pair.clip);
        }
    }

    private void OnEnable()
    {
        foreach (StringAudioPair pair in _clipList)
        {
            MessageDispatcher.AddListener(pair.key, Play);
        }
    }

    private void OnDisable()
    {
        foreach (StringAudioPair pair in _clipList)
        {
            MessageDispatcher.RemoveListener(pair.key, Play);
        }
    }


    private void Play(IMessage rMessage)
    {
        if(_clipDict.ContainsKey(rMessage.Type))
        {
            _audioSource.PlayOneShot(_clipDict[rMessage.Type]);
        }
    }
}
