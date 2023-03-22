using UnityEngine;

public sealed class Projectile : MonoBehaviour
{
    [SerializeField] float speed;

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        if (transform.position.y < 0)
        {
            Destroy(gameObject);
        }
    }
}