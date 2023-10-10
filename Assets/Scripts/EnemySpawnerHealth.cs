using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnerHealth : MonoBehaviour
{

    private bool _isDead = false;
    public float _health;
    [SerializeField] private Slider _healthSlider;

    void Update()
    {
        DisplayHealth();
        CheckIfDead();
    }

    void DisplayHealth()
    {
        _healthSlider.value = _health;
    }
    void CheckIfDead()
    {
        if(_health <= 0.0000f)
        {
            Dead();
            StopSpawning();
        }
    }
    void StopSpawning()
    {
        _isDead = true;
        
    }
    public void DecreaseHealth(float damage)
    {
        _health -= damage;
    }

    public void Dead()
    {
        gameObject.SetActive(false);
        //play sound
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Bullet"))
        {
            _health -= 1f;
            if(_health == 0)
            {
                Destroy(gameObject);
                Debug.Log("destroyed");
            }
            Destroy(other.gameObject, 0.05f);
        }
    }
}
