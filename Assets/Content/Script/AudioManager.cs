using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{

    public AudioSource effect;

    [System.Serializable]
    public class AudioData
    {
        public audioType type;
        public AudioClip clip;

    }

    public List<AudioData> data = new List<AudioData>();

    private Dictionary<audioType, AudioClip> clips = new Dictionary<audioType, AudioClip>();


    private void Start()
    {
        // inisiasi data Dictionary
        foreach(var clip in data)
        {
            if (!clips.ContainsKey(clip.type))
            {
                clips.Add(clip.type, clip.clip);
            }
        }
    }
    /// <summary>
    /// play audio effect
    /// </summary>
    /// <param name="type">type audio</param>
    public void playClip(audioType type)
    {
        if(clips.TryGetValue(type, out AudioClip clip))
        {
            effect.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("Audio not found for: " + type);
        }
    }

}
public enum audioType
{
    Cookies,
    Sell,
    Buy,
    ResfreshQuest,
    CompleteQuest,
    UIClick
}