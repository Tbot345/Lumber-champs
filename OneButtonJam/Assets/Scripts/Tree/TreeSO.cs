using UnityEngine;

[CreateAssetMenu(fileName = "Tree", menuName = "Scriptable Objects/TreeSO")]
public class TreeSO : ScriptableObject
{
    public int health;
    public int reward;
    public string name;
}
