using UnityEngine;
using UnityEditor;

public class BatchProccessor : MonoBehaviour
{
    [SerializeField] private CharacterSO[] characters;
    [SerializeField] private AnimationData[] idleAnimations;
    [SerializeField] private AnimationData[] chopAnimations;

    [ContextMenu("Assign Animations to Characters")]
    private void AssignAnimations()
    {
        if (characters.Length != idleAnimations.Length || characters.Length != chopAnimations.Length)
        {
            Debug.LogError("The number of characters and animations do not match.");
            return;
        }

        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].idle = idleAnimations[i];
            characters[i].chop = chopAnimations[i];
            EditorUtility.SetDirty(characters[i]);
        }

        Debug.Log("Animations assigned successfully.");
    }
}
