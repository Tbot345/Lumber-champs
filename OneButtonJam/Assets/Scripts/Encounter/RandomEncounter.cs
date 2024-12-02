using UnityEngine;

public class RandomEncounter : MonoBehaviour
{
    [SerializeField] private GameObject grandmaPrefab;
    [SerializeField] private GameObject[] treeEncounterObjects;

    public void ChooseRandomEncounter(bool spawnAfterTreeChopped, Vector3 lastPosition)
    {
        if(spawnAfterTreeChopped == false)
        {
            Instantiate(grandmaPrefab, lastPosition, Quaternion.identity);
        } else
        {
            int randomEncounterIndex2 = Random.Range(0, treeEncounterObjects.Length);
            Instantiate(treeEncounterObjects[randomEncounterIndex2], lastPosition, Quaternion.identity);
        }
    }
}
