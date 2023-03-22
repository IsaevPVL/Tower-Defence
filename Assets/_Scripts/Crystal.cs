using UnityEngine;
using DG.Tweening;

public sealed class Crystal : MonoBehaviour
{
    [Header("Attacks"), SerializeField] PlayerStats stats;
    [SerializeField] LayerMask enemyLayermask;
    [SerializeField] GameObject projectile;
    float nextAttackTime;
    Collider[] enemiesInRange = new Collider[32];
    IDamageable enemyToAttack;
    Vector3 targetPosition;
    bool canAttack = true;


    [Header("Animation"), SerializeField] float breathingHeight;
    [SerializeField] float breathingDuration;
    [SerializeField] Ease breathingEase;
    [SerializeField] float singleRotationDuration;
    Vector3 rotationVector = new Vector3(360f, 360f, 0);

    [Header("Range Circle"), SerializeField] LineRenderer lineRenderer;
    [SerializeField] int segmentCount = 18;
    [SerializeField] float lineWidth = 0.1f;
    float angle;
    Vector3[] positions;
    Vector3 currentPosition = new Vector3(0f, 0.15f, 0f);

    void Start()
    {
        transform.DOMoveY(breathingHeight, breathingDuration)
        .SetRelative()
        .SetEase(breathingEase)
        .SetLoops(-1, LoopType.Yoyo);

        transform.DORotate(rotationVector, singleRotationDuration)
        .SetRelative()
        .SetEase(Ease.Linear)
        .SetLoops(-1, LoopType.Restart);

        DrawAttackRange();
    }

    void Update()
    {
        if (!canAttack || (Time.time < nextAttackTime))
        {
            return;
        }

        enemyToAttack = FindClosestEnemy(Physics.OverlapSphereNonAlloc(Vector3.zero, stats.AttackRange, enemiesInRange, enemyLayermask));

        if (enemyToAttack != null)
        {
            Instantiate(projectile, transform.position, Quaternion.LookRotation(targetPosition - transform.position));

            DOVirtual.DelayedCall(.1f, ()=> enemyToAttack.TakeDamage(stats.AttackDamage));
        }

        nextAttackTime = Time.time + 1f / stats.AttackSpeed;
    }

    IDamageable FindClosestEnemy(int enemies)
    {
        if (enemies == 0)
        {
            return null;
        }

        float shortestSquared = float.MaxValue;
        int index = 0;

        for (int i = 0; i < enemies; i++)
        {
            float distance = (Vector3.zero - enemiesInRange[i].transform.position).sqrMagnitude;

            if (distance < shortestSquared)
            {
                shortestSquared = distance;
                index = i;
            }
        }

        targetPosition = enemiesInRange[index].transform.position;
        return enemiesInRange[index].gameObject.GetComponent<IDamageable>();
    }

    public void DrawAttackRange()
    {
        positions = new Vector3[segmentCount + 1];
        lineRenderer.positionCount = segmentCount + 1;
        lineRenderer.startWidth = lineRenderer.endWidth = lineWidth;

        for (int i = 0; i < positions.Length; i++)
        {
            currentPosition.x = Mathf.Sin(Mathf.Deg2Rad * angle) * stats.AttackRange;
            currentPosition.z = Mathf.Cos(Mathf.Deg2Rad * angle) * stats.AttackRange;

            angle += 360f / (segmentCount);

            positions[i] = currentPosition;
        }
        lineRenderer.SetPositions(positions);
    }

    public void StopAttacking()
    {
        canAttack = false;
    }
    public void StartAttacking()
    {
        canAttack = true;
        nextAttackTime = Time.time + 1f / stats.AttackSpeed;
    }
}