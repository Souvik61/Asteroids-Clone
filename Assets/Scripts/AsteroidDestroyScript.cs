using UnityEngine;

public class AsteroidDestroyScript : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Destroy(collider.gameObject);
    }

}
