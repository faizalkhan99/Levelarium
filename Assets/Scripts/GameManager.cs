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


    [SerializeField] GameObject GatePrefab, KeyPrefab;
    [SerializeField] Transform GatePos, KeyPos;

    public bool FellIntoVoid = false;
    public bool IsLevelFailed = false;
    public bool IsPlayerDead = false;

    public void InstantiateGate()
    {
        Instantiate(GatePrefab, GatePos.position, GatePos.rotation);
    }

    public void InstantiateKey()
    {
        Instantiate(KeyPrefab, KeyPos.position, Quaternion.identity);
    }
}
