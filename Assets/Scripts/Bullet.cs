using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    
}
