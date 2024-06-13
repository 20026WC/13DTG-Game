using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public GameObject left;
    public GameObject right;
    public GameObject groundAttack;
    public GameObject Sword;
    public GameObject Spikedball;
    public GameObject Canva;
    public HealthBar healthBar;
    public Button RestartButton;
    public TextMeshProUGUI gameoverText;

    public int maxHealth = 100;
    public int currentHealth;
    public int Thedifficulty;

    public float gravityModifier;
    public float horizontalInput;
    public float forwardInput;
    public float turnSpeed = 50.0f;
    public float speed;
    public float jumpForce;

    public bool PlayerAttacking = false;
    public bool PlayerDashingToLeft = false;
    public bool PlayerDashingToRight = false;
    public bool PlayerSmashedGround = false;
    public bool SwordDuel = false;
    public bool LightAttack = false;
    public bool SpikedChainAttack = false;
    public bool isOnGround = true;
    public bool CanDodge = true;
    public bool GameIsActive = false;
    public bool EndlessGameIsActive = false;  
    public bool NormalGameIsActive = false;
    public bool BeginBattle = false;
    public bool PlayerIsDead = false;

    private Rigidbody playerRb;
    private SpawnManager SpawnManager;

    // Start is called before the first frame update
    void Start()
    {
        //This pulls the spawnmanager script from the spawn manager game object. 
        SpawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameIsActive == true)
        {
            //This allows the player model to move.
            horizontalInput = Input.GetAxis("Horizontal");
            forwardInput = Input.GetAxis("Vertical");

            transform.Translate(Vector3.forward * Time.deltaTime * turnSpeed * forwardInput);
            transform.Translate(Vector3.right * Time.deltaTime * turnSpeed * horizontalInput);

            //This rotates the player when Q is pressed.
            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
            }

            // Rotate the player when E key is pressed
            if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);

                StartCoroutine(DashCountdownRoutine());
            }


            //This is the code to makes the player jump when space is pressed
            if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
            {
                //This is the code for the player to jump.
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                //This code makes sure the player only able to jump if their on the ground.
                isOnGround = false;
            }

            //This mkaes it so the player dies when they fall off the platforms. 
            if (transform.position.y < -91.8)
            {
                GameOver();
            }     

            if (transform.position.x < 1)
            {
                BeginBattle = true;
            }

            //This lets the player dodge towards the right when right shift is pressed. 
            if (Input.GetKeyDown(KeyCode.RightShift) && CanDodge)
            {
                PlayerDashingToRight = true;
                StartCoroutine(DashCountdownRoutine());
            }

            //This lets the player dodge towards the left when left shift is pressed.
            if (Input.GetKeyDown(KeyCode.LeftShift) && CanDodge)
            {
                PlayerDashingToLeft = true;
                StartCoroutine(DashCountdownRoutine());
            }       
            
            //This is the code for when teh player attacks from the sky. 
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //This makes it so the player does a large attack when on ground.
                if (isOnGround == true)
                {
                    SwordDuel = true;
                    StartCoroutine(PlayerAttackCountdownRoutine());
                }
                
                //This allows the player to do a different attack when in the sky. 
                if (isOnGround == false)
                {
                    playerRb.AddForce(Vector3.down * 40, ForceMode.Impulse);
                    PlayerSmashedGround = true;
                    StartCoroutine(DashCountdownRoutine());
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                LightAttack = true;
                StartCoroutine(PlayerAttackCountdownRoutine());
            }

            //This is the code for what happens when the player has a health of 0. 
            if (currentHealth < 0)
            {
                //This calls the function of GameOver()
                GameOver();
            }

        }

    }

    void Damage(int damage)
    {
        //This is the code for damage is minused form the player's health. 
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    public void GameOver()
    {
        gameObject.SetActive(false);
        PlayerIsDead = true;
        gameoverText.gameObject.SetActive(true);
        RestartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        Destroy(GameObject.Find("Player"));
        //This loads the the begining scenes.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //The below line of code is there so the player's gravity modifier is not incrased when they teleport. 
        Physics.gravity /= gravityModifier;
    }

    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemie"))
        {
            //this is the damage that the enemie will do to the player when they collide. 
                Damage(5 * Thedifficulty);
 
        }      
        if (other.gameObject.CompareTag("Destroyer"))
        {
            //This is the code that destorys the enemies projectiles when it hits the player
            //It also multiplys the diffuclty that teh player chose. 
            Damage(2 * Thedifficulty);
            Destroy(other.gameObject);
        }     
        if (other.gameObject.CompareTag("Potion"))
        {
            //This is the code for when the player touches the potion. 
            currentHealth = maxHealth;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("First Boss"))
        {
            Damage(5 * Thedifficulty);
        }  
        if (other.gameObject.CompareTag("water"))
        {
            GameOver();
        }      

    }

   
    //This is 
    public void NormalStartGame(int difficulty)
    {
        StartGame();
        NormalGameIsActive = true;
        SpawnManager.StartOfGame();
        Thedifficulty = difficulty;
    }  
    
    //Thisd preapres the game for when the enedless game is chosen. 
    public void EndlessStartGame()
    {
        StartGame();
        EndlessGameIsActive = true;
        SpawnManager.StartOfGame();
    }

    //This is the code to prepare the player for when the game is started.
    public void StartGame()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        GameIsActive = true;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        PlayerIsDead = false;
        GameIsActive = true;
        Canva.gameObject.SetActive(true);
        currentHealth = maxHealth;
    }



    IEnumerator PlayerAttackCountdownRoutine()
    {
        if (SwordDuel == true)
        {
            PlayerAttacking = true;
            Sword.SetActive(true);
            yield return new WaitForSeconds(1);
            Sword.SetActive(false);
            PlayerAttacking = false;
            SwordDuel = false;
        }   
        
        if (LightAttack == true)
        {
            PlayerAttacking = true;
            left.SetActive(true);
            right.SetActive(true);
            yield return new WaitForSeconds(1);
            left.SetActive(false);
            right.SetActive(false);
            PlayerAttacking = false;
            LightAttack = false;
        }

    }

    IEnumerator DashCountdownRoutine()
    {
        if (PlayerDashingToLeft == true)
        {
            left.SetActive(true);
            transform.position = left.transform.position;
            yield return new WaitForSeconds(1);
            PlayerAttacking = false;
            left.SetActive(false);
            PlayerDashingToLeft = false;
            CanDodge = false;
            yield return new WaitForSeconds(2);
            CanDodge = true;

        }

        if (PlayerDashingToRight == true)
        {
            PlayerAttacking = true;
            right.SetActive(true);
            transform.position = right.transform.position;
            yield return new WaitForSeconds(1);
            right.SetActive(false);
            PlayerAttacking = false;
            PlayerDashingToRight = false;
            CanDodge = false;
            yield return new WaitForSeconds(2);
            CanDodge = true;
        }

        if (PlayerSmashedGround == true)
        {
            groundAttack.SetActive(true);
            yield return new WaitForSeconds(1);
            groundAttack.SetActive(false);
            PlayerSmashedGround = false;
        }


    }
}
