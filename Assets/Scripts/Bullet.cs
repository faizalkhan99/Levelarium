using UnityEngine;

public class Bullet : MonoBehaviour
{

    private void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    
}
