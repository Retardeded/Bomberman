using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public int playerHealth = 20;
    public float playerSpeed = 5f;
    public int playerDamage = 1;
    public int numberOfBombs = 1;
    public Explosion bomb;
    public Animator playerAnimator;
    public Renderer DeathState;

    Rigidbody2D rb;
    Renderer playerRenderer;
    public int playerNumber = 1;
    public int availableBombs;
    bool isDead = false;
    IEnumerator DeathCoroutine()
    {
        isDead = true;
        Instantiate(DeathState, transform.position, Quaternion.identity);
        playerRenderer.enabled = false;
        yield return new WaitForSeconds(2f);
        GameManager.Instance.RestartTheGame(playerNumber);
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        playerAnimator.SetTrigger("isDamaged");
        if (playerHealth <= 0)
        {
            StartCoroutine("DeathCoroutine");
        }
    }
    public void GetBoost(int num)
    {
        if (num == 0)
            playerSpeed += 0.5f;
        if (num == 1)
            playerDamage++;
        if (num == 2)
        {
            numberOfBombs++;
            availableBombs++;
        }
    }
    public void GetBombBack()
    {
        availableBombs++;
    }
	void Start () {
        playerRenderer = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody2D>();
        availableBombs = numberOfBombs;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isDead)
            return;
        HandleInput();
    }

    private void HandleInput()
    {
        float horizontal = Input.GetAxis(playerNumber + "Horizontal");
        float vertical = Input.GetAxis(playerNumber + "Vertical");

        Vector2 newVelocity = new Vector2(horizontal * playerSpeed, vertical * playerSpeed);

        if (Input.GetButtonDown(playerNumber + "Attack") && availableBombs > 0)
        {
            Explosion newBomb = Instantiate(bomb, this.transform.position, Quaternion.identity);
            newBomb.explosionRadius = playerDamage;
            playerAnimator.SetTrigger("isBombing");
            availableBombs--;
            Invoke("GetBombBack", 3f);
        }
        rb.velocity = newVelocity;
    }
}
