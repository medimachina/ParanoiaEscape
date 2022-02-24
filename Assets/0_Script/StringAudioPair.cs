using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public struct StringAudioPair
{
    [HorizontalGroup("pair"), LabelWidth(30)]
    public string key;
    [HorizontalGroup("pair"), LabelWidth(30)]
    public AudioClip clip;
}
