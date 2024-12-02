using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSO", menuName = "Scriptable Objects/CharacterSO")]
public class CharacterSO : ScriptableObject
{
    [Header("Character Selection")]
    public Sprite textImage;
    public Sprite characterImage;

    [Header("Animations")]
    public AnimationData idle;
    public AnimationData chop;
}
