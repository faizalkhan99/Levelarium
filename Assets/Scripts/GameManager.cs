using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null) Debug.Log("Game Manager was NULL");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    ///Properties
    public bool IsSpawnerDead { get; set; }
    public bool HasKey { get; set; }


    [SerializeField] GameObject _gatePrefab, _keyPrefab;
    [SerializeField] Transform _gatePos, _keyPos;

    public void InstantiateGate()
    {
        Instantiate(_gatePrefab, _gatePos.position, _gatePos.rotation);
    }

    public void InstantiateKey()
    {
        Instantiate(_keyPrefab, _keyPos.position, Quaternion.identity);
    }

}
