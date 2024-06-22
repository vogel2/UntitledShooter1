using UnityEngine;
using UnityEngine.AI;

public class enemyController2 : MonoBehaviour
{
    public float speed = 2f;
    public float attackRange = 1.5f;
    public float playerDetectionRange = 5f;
    public float damage = 10f;
    public float attackInterval = 1f;

    private Transform upgradePoint;
    private Transform player;
    private PlayerHealth playerHealth;
    private UpgradePointController upgradePointController;
    private NavMeshAgent navAgent;
    private enemyAnimaitor animController;
    private float nextAttackTime = 0f;

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth == null)
            {
                Debug.LogError("PlayerHealth component not found on Player GameObject.");
            }
        }
        else
        {
            Debug.LogError("Player GameObject not found. Ensure the player is tagged 'Player'.");
        }

        GameObject upgradePointObj = GameObject.FindWithTag("UpgradePoint");
        if (upgradePointObj != null)
        {
            upgradePointController = upgradePointObj.GetComponent<UpgradePointController>();
            if (upgradePointController == null)
            {
                Debug.LogError("UpgradePointController component not found on UpgradePoint GameObject.");
            }
            upgradePoint = upgradePointObj.transform;
        }
        else
        {
            Debug.LogError("UpgradePoint GameObject not found. Ensure the upgrade point is tagged 'UpgradePoint'.");
        }

        navAgent = GetComponent<NavMeshAgent>();
        if (navAgent != null)
        {
            navAgent.speed = speed;
            navAgent.stoppingDistance = attackRange;
        }
        else
        {
            Debug.LogError("NavMeshAgent component not found on the enemy GameObject.");
        }

        animController = GetComponent<enemyAnimaitor>();
        if (animController == null)
        {
            Debug.LogError("enemyAnimaitor component not found on the enemy GameObject.");
        }

        // Set initial target to upgrade point
        SetTarget(upgradePoint);
        animController.Walk(true);
    }

    void Update()
    {
        if (player == null || upgradePoint == null || navAgent == null)
        {
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        float distanceToUpgradePoint = Vector3.Distance(transform.position, upgradePoint.position);

        if (distanceToUpgradePoint <= attackRange)
        {
            // Attack the upgrade point if in range
            SetTarget(upgradePoint);
            TryAttack(upgradePointController);
        }
        else if (distanceToPlayer <= playerDetectionRange)
        {
            // Attack the player if within detection range
            SetTarget(player);
            TryAttack(playerHealth);
        }
        else
        {
            // Default to attacking the upgrade point
            SetTarget(upgradePoint);
        }

        UpdateAnimationState();
    }

    void SetTarget(Transform newTarget)
    {
        if (navAgent.destination != newTarget.position)
        {
            navAgent.SetDestination(newTarget.position);
        }
    }

    void UpdateAnimationState()
    {
        if (navAgent.velocity.sqrMagnitude > 0)
        {
            animController.Walk(true);
        }
        else
        {
            animController.Walk(false);
        }
    }

    void TryAttack(Component targetComponent)
    {
        if (targetComponent == null) return;

        if (Time.time >= nextAttackTime && Vector3.Distance(transform.position, targetComponent.transform.position) <= attackRange)
        {
            animController.Attack();

            if (targetComponent.CompareTag("Player"))
            {
                playerHealth.applyDamage(damage);
            }
            else if (targetComponent.CompareTag("UpgradePoint"))
            {
                upgradePointController.TakeDamage(damage);
            }

            nextAttackTime = Time.time + attackInterval;
        }
    }

    
}
