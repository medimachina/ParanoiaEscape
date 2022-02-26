using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.ootii.Messages;
using UnityEngine.Audio;
using System;
using Sirenix.OdinInspector;
using DG.Tweening;

public class MusicPlayer : SingletonBase<MusicPlayer>
{
    [SerializeField]
    private AudioClip _defaultMusic, _actionStartMusic, _actionLoopMusic, _menuMusic;
    [SerializeField]
    private AudioMixerGroup _mixerGroup;

    private AudioSourceController _defaultPlayer;
    private AudioSourceController _actionPlayer;
    private AudioSourceController _menuPlayer;

    private List<AudioSourceController> _sourceControllers;

    // Start is called before the first frame update
    void Start()
    {
        _defaultPlayer = new AudioSourceController(this, gameObject.AddComponent<AudioSource>(), _defaultMusic, _mixerGroup);

        _actionPlayer = new AudioSourceController(this, gameObject.AddComponent<AudioSource>(), _actionLoopMusic, _mixerGroup);
        _actionPlayer.Intro = _actionStartMusic;
        _actionPlayer.FadeOutTime = 2;
        _menuPlayer = new AudioSourceController(this, gameObject.AddComponent<AudioSource>(), _menuMusic, _mixerGroup);

        _sourceControllers = new List<AudioSourceController>();
        _sourceControllers.Add(_defaultPlayer);
        _sourceControllers.Add(_actionPlayer);
        _sourceControllers.Add(_menuPlayer);
    }

    void OnEnable()
    {
        MessageDispatcher.AddListener(Msg.AlarmStarted, OnAlarmStarted);
        MessageDispatcher.AddListener(Msg.AlarmStopped, OnAlarmStopped);
        MessageDispatcher.AddListener(Msg.WonGame, OnLevelEnded);
        MessageDispatcher.AddListener(Msg.LostGame, OnLevelEnded);
        MessageDispatcher.AddListener(Msg.LevelStarted, OnLevelStarted);
        Debug.Log($"Music: {name} Registering message {Msg.LevelStarted}");

    }
    private void OnDisable()
    {
        MessageDispatcher.RemoveListener(Msg.AlarmStarted, OnAlarmStarted);
        MessageDispatcher.RemoveListener(Msg.AlarmStopped, OnAlarmStopped);
        MessageDispatcher.RemoveListener(Msg.WonGame, OnLevelEnded);
        MessageDispatcher.RemoveListener(Msg.LostGame, OnLevelEnded);
        MessageDispatcher.RemoveListener(Msg.LevelStarted, OnLevelStarted);
        Debug.Log($"Music: {name} Unregistering message {Msg.LevelStarted}");
    }

    [Button("Level Started")]
    private void OnLevelStarted(IMessage rMessage)
    {
        Debug.Log("On level started!");
        FadeToPlayer(_defaultPlayer);
    }

    [Button("Level Ended")]
    private void OnLevelEnded(IMessage rMessage)
    {
        FadeToPlayer(_menuPlayer);
    }

    [Button("Alarm Started")]
    private void OnAlarmStarted(IMessage rMessage)
    {
        FadeToPlayer(_actionPlayer);
    }

    [Button("Alarm Stopped")]
    private void OnAlarmStopped(IMessage rMessage)
    {
        FadeToPlayer(_defaultPlayer);
    }

    void FadeToPlayer(AudioSourceController sourceController)
    {
        FadeOutAllExcept(sourceController);
        sourceController.FadeIn();
    }

    void FadeOutAllExcept(AudioSourceController sourceControllerToKeep)
    {
        foreach (AudioSourceController ctrlr in _sourceControllers)
        {
            if (ctrlr != sourceControllerToKeep)
            {
                ctrlr.FadeOut();
            }
        }

    }

}

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
        _source.loop = false;
        _source.Stop();
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
