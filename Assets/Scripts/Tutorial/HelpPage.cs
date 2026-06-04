using UnityEngine;
using UnityEngine.Video;

[System.Serializable]
public class HelpPage
{
    public string title;

    [TextArea]
    public string description;

    public VideoClip video;
}