using UnityEngine;

public class DropLoot : MonoBehaviour
{
    [SerializeField] private GameObject[] _lootPrefab;

    public void LootProbability()
    {
        int drop = Random.Range(0,1);
       
        if(drop == 1)
        {
            Debug.Log("Loot Dropped");
            Instantiate(_lootPrefab[Random.Range(0, _lootPrefab.Length)], transform.position, Quaternion.identity);
        }
        else
        {
            return;
        }
        //_ = (drop == 1) ? Instantiate(_lootPrefab[Random.Range(0, _lootPrefab.Length)], transform.position, Quaternion.identity) : null;

    }
}
