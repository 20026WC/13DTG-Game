using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseRangeEnemei : MonoBehaviour
{
    public int maxHealth = 50;
    public int currentHealth;
    public HealthBar healthBar;
    public float speed = 3.0f;
    private Rigidbody enemyRb;
    private PlayerController PlayerController;
    private Vector3 offset = new Vector3(2, 0, 2);

    public Transform Player;
    public UnityEngine.AI.NavMeshAgent agent;
    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    bool alreadyAttacked;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    private float powerupStrength = 15.0f;


    // Start is called before the first frame update
    void Start()
    {
        //Calls all the nesscary infomation at the start. 
        //This gets the rigidbody from the enemy
        enemyRb = GetComponent<Rigidbody>();
        //This grabs the player controller script off of player. 
        PlayerController = GameObject.Find("Player").GetComponent<PlayerController>();
        Player = GameObject.Find("Player").transform;

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //This mkaes the enemie have full health when it spawns. 
        healthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        //The paths the enemie can choose to go through. 
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();


        //This destroys the game object when the nemies health lowers to 0
        if (currentHealth < 0)
        {
            Destroy(gameObject);
        }

        

    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;

    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }
    private void ChasePlayer()
    {
        //Walks towards player.
        agent.SetDestination(Player.position);
    }

    private void AttackPlayer()
    {
       // This is the code for a close range enemie to attack. 
            agent.SetDestination(transform.position);
            transform.LookAt(Player);
            alreadyAttacked = true;
            agent.SetDestination(transform.position + offset);
        


        if (!alreadyAttacked)
        {
            //This calls the rest attack function. 
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    //This is the function to reset already attacked.
    private void ResetAttack()
    {
        StartCoroutine(AttackCountdownRoutine());
        alreadyAttacked = false;


    }


    void Damage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    //These are the codes for when the enmie is attcked. 
    private void OnTriggerEnter(Collider other)
    {

        //This is the line of code for when the player's sword attacks enemie. 
        if (other.gameObject.CompareTag("Player Weapon"))
        {
            Damage(30);
        }
        //This is when the player attacks from the sky. 
        if (other.gameObject.CompareTag("Dash Area"))
        {
            Damage(10);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player Weapon"))
        {
            Rigidbody Rigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            Rigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

    }


//This is the code for a countdown before attacks reset.
    IEnumerator AttackCountdownRoutine()
    {
        yield return new WaitForSeconds(1);
    }
}
