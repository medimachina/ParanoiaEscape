using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using DG.Tweening;

public class AudioSourceController
{
    public MonoBehaviour _parentBehavour;
    public AudioClip MainClip;
    public AudioClip Intro;
    public AudioClip Outro;
    public float FadeInTime = 1;
    public float FadeOutTime = 1;
    AudioSource _source;
    Coroutine _waitingToEnd;
    private bool _isPlaying = false;
    private Tween currentTween;

    public bool IsPlaying => _isPlaying;

    public AudioSourceController(MonoBehaviour parentBehavour, AudioSource source, AudioClip mainClip, AudioMixerGroup mixerGroup)
    {
        _parentBehavour = parentBehavour;
        _source = source;
        _source.outputAudioMixerGroup = mixerGroup;
        MainClip = mainClip;

        CleanUp();
    }

    public void PlayQueue(Queue<AudioClip> playQueue)
    {
        if (playQueue.Count == 0)
        {
            _isPlaying = false;
            return;
        }
        else
        {
            _isPlaying = true;
        }

        _source.clip = playQueue.Dequeue();
        _source.Play();

        Debug.Log($"Music: Playing clip {_source.clip}. {playQueue.Count} left in queue");

        if (_source.clip == MainClip && playQueue.Count == 0)
        {
            Debug.Log($"Music: {_source.clip} is main clip and playQueue count = {playQueue.Count} => Setting to loop");
            _source.loop = true;
            return;
        }

        if (playQueue.Count > 0)
        {
            Debug.Log($"Cout {playQueue.Count} > 0. Starting coroutine with playing next at end.");
            _waitingToEnd = _parentBehavour.StartCoroutine(WaitForClipToEndCo(_source, () =>
            {
                PlayQueue(playQueue);
            }));
        }
        else
        {
            Debug.Log($"Cout {playQueue.Count} == 0. Starting coroutine with setting _isPlaying to false at end.");
            _waitingToEnd = _parentBehavour.StartCoroutine(WaitForClipToEndCo(_source, () =>
            {
                _isPlaying = false;
            }));
        }
    }

    public void Play()
    {
        if (!_isPlaying)
        {
            Queue<AudioClip> queue = GenerateQueue();
            Debug.Log($"Music: Creating a queue with {queue.Count} tracks");
            PlayQueue(queue);
        }
    }

    private Queue<AudioClip> GenerateQueue()
    {
        Queue<AudioClip> queue = new Queue<AudioClip>();
        EnqueIfExists(queue, Intro);
        EnqueIfExists(queue, MainClip);
        EnqueIfExists(queue, Outro);

        return queue;
    }

    private void EnqueIfExists(Queue<AudioClip> queue, AudioClip clip)
    {
        if (clip != null)
        {
            queue.Enqueue(clip);
        }
    }

    private void CleanUp()
    {
        if (_waitingToEnd != null)
        {
            _parentBehavour.StopCoroutine(_waitingToEnd);
        }
        if (currentTween != null)
        {
            currentTween.Kill();
        }
        if (_source.isPlaying)
        {
            _source.Stop();
        }
        _source.loop = false;
        _source.volume = 0;
    }

    public void FadeOut()
    {
        CleanUp();

        if (IsPlaying)
        {
            currentTween = _source.DOFade(0, FadeOutTime).OnComplete(() =>
            {
                _source.Stop();
            });
            _isPlaying = false;
        }
    }

    public void FadeIn()
    {
        if (!IsPlaying)
        {
            CleanUp();
            Play();
            currentTween = _source.DOFade(1, 1);
        }
    }

    IEnumerator WaitForClipToEndCo(AudioSource source, Action actionOnEnd)
    {
        while (source.isPlaying == true)
        {
            yield return 0;
        }

        actionOnEnd();
    }
}
