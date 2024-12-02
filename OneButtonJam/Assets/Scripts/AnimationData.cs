using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "AnimationData", menuName = "Scriptable Objects/AnimationData")]
public class AnimationData : ScriptableObject
{
    public string animationName;
    public List<Sprite> frames;
    public bool loop;
    public float timeBetweenFrames;
}
