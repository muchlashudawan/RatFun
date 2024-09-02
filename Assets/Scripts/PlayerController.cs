using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    bool isJump = true;
    bool isDead = false; // Added back the isDead variable
    int idMove = 0;
    Animator anim;
    bool facingRight = true;

    public AudioClip coinCollectSound; // Audio clip for coin collection

    // Use this for initialization
    private void Start()
    {
        anim = GetComponent<Animator>();
        EnemyController.EnemyKilled = 0; // Reset enemy killed count
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead) // Check if the player is not dead before allowing movement
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveLeft();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveRight();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                Idle();
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                Idle();
            }
            Move();
        }
        Dead(); // Check if the player has fallen off the map
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Condition when touching the ground
        if (isJump)
        {
            anim.ResetTrigger("jump");
            if (idMove == 0) anim.SetTrigger("idle");
            isJump = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Condition when leaving the ground
        anim.SetTrigger("jump");
        anim.ResetTrigger("run");
        anim.ResetTrigger("idle");
        isJump = true;
    }

    public void MoveRight()
    {
        idMove = 1;
        if (!facingRight)
        {
            Flip();
        }
    }

    public void MoveLeft()
    {
        idMove = 2;
        if (facingRight)
        {
            Flip();
        }
    }

    private void Move()
    {
        float movementSpeed = 2.5f;

        if (idMove == 1 && !isDead)
        {
            // Condition when moving right
            if (!isJump) anim.SetTrigger("run");
            transform.Translate(Vector3.right * Time.deltaTime * movementSpeed);
        }
        if (idMove == 2 && !isDead)
        {
            // Condition when moving left
            if (!isJump) anim.SetTrigger("run");
            transform.Translate(Vector3.left * Time.deltaTime * movementSpeed);
        }
    }

    public void Jump()
    {
        if (!isJump)
        {
            // Condition when jumping
            anim.SetTrigger("run");  // Set run animation state
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 280f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            Data.score += 15; // Update score when player collects a coin
            AudioSource.PlayClipAtPoint(coinCollectSound, transform.position);
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            // Trigger game over condition
            isDead = true;
            SceneData.previousSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("Game Over");
        }
    }

    public void Idle()
    {
        // Condition when idle
        if (!isJump)
        {
            anim.ResetTrigger("jump");
            anim.ResetTrigger("run");
            anim.SetTrigger("idle");
        }
        idMove = 0;
    }

    private void Dead()
    {
        if (!isDead)
        {
            if (transform.position.y < -10f)
            {
                // Condition when falling off the map
                isDead = true;
                SceneData.previousSceneName = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene("Game Over");
            }
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
