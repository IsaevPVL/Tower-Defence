using UnityEngine;
using System;

public sealed class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] float movementSpeed;
    [SerializeField] float maxHealth;
    [SerializeField] Rigidbody rb;
    float health;
    bool isMoving = true;

    public static event Action OnEnemyDying;
    public static event Action OnReachingCastle;

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        if (isMoving)
        {
            rb.MovePosition(rb.position + transform.forward * movementSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Castle"))
        {
            OnReachingCastle?.Invoke();
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0f)
        {
            OnEnemyDying?.Invoke();

            Destroy(gameObject);
        }
    }

    void StopMovement()
    {
        isMoving = false;
    }

    void OnEnable()
    {
        Enemy.OnReachingCastle += StopMovement;
    }

    void OnDisable()
    {
        Enemy.OnReachingCastle -= StopMovement;
    }
}