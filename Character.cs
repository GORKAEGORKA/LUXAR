using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Character : Unit
{
    public GameObject finishPanel;
    public GameObject pause;
    public GameObject diePanel;
    public Text coinText;
    public GameObject respawn;

    [SerializeField]
    public int score = 0;

    [SerializeField]
    private int lives = 5;

    public int Lives
    {
        get { return lives; }
        set
        {
            if (value < 6) lives = value;
            livesBar.Refresh();
        }
    }
    private LivesBar livesBar;

    [SerializeField]
    private float speed = 3.0F;
    [SerializeField]
    private float jumpForce = 15.0F;

    private bool isGrounded = false;

    private Bullet bullet;

    private CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int) value); }
    }

    new private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer sprite;

    private void Awake()
    {
        
        livesBar = FindObjectOfType<LivesBar>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        bullet = Resources.Load<Bullet>("Bullet");

        UnityEngine.Cursor.visible = false;
    }
    
    private void FixedUpdate()
    {
        
        CheckGround();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && pause.activeSelf == false && finishPanel.activeSelf == false) Shoot();
        if (isGrounded) State = CharState.Idle;
        if ((Input.GetKeyUp("s")) || (Input.GetKeyUp("down")) && pause.activeSelf == false && finishPanel.activeSelf == false) speed = 3.0F;
        if (Input.GetButton("Horizontal") && pause.activeSelf == false && finishPanel.activeSelf == false) Run();
        if (isGrounded && Input.GetButtonDown("Jump") && pause.activeSelf == false && finishPanel.activeSelf == false) Jump();
        if (isGrounded && Input.GetButton("Vertical") && pause.activeSelf == false && finishPanel.activeSelf == false) Squat();
        coinText.text = "COINS " + score;

        

        if (pause.activeSelf == true || finishPanel.activeSelf == true)
        {
            UnityEngine.Cursor.visible = true;
        } else if (pause.activeSelf == false || finishPanel.activeSelf == false)
        {
            UnityEngine.Cursor.visible = false;
        }

        if (pause.activeSelf == true && Input.GetKeyDown(KeyCode.Escape))
        {
            pause.gameObject.SetActive(false);
        } else if (pause.activeSelf == false && Input.GetKeyDown(KeyCode.Escape))
        {
            pause.gameObject.SetActive(true);
        } 
    }

    private void Run()
    {
        
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
       

        sprite.flipX = direction.x < 0.0F;
        
        if (isGrounded) State = CharState.Run;
    }

    private void Jump()
    {
        rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);

       
    }

    private void Squat()
    {
        if (Input.GetAxis("Vertical") < 0) speed = 0.0F;
        if (Input.GetAxis("Vertical") < 0) State = CharState.Squat;
       
        
    }

    private void Shoot()
    {
        Vector3 position = transform.position;   position.y += 1.53F; 
        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;
        newBullet.Parent = gameObject;
        newBullet.Direction = newBullet.transform.right * (sprite.flipX ? -1.0F : 1.0F);
        
        
    }

    public override void ReceiveDamage()
    {
        Lives--;

        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(transform.up * 14.5F, ForceMode2D.Impulse);

        if (lives == 0)
        {
            Die();
        }

         
        
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3F);

        isGrounded = colliders.Length > 1;

        if (!isGrounded) State = CharState.Jump;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //Unit unit = collider.gameObject.GetComponent<Unit>();
        //if (unit) ReceiveDamage();

        Bullet bullet = collider.gameObject.GetComponent<Bullet>();
        if (bullet && bullet.Parent != gameObject) ReceiveDamage();

        if (collider.gameObject.tag == "coin")
        {
            Destroy(collider.gameObject);
            score++;
        }

        if (collider.gameObject.tag == "heart")
        {
            Destroy(collider.gameObject);
            
            Lives++;
            
            
        }

        if (collider.gameObject.tag == "DieSpace")
        {
            Die();
        }

        if (collider.gameObject.tag == "Finish")
        {
            finishPanel.SetActive(true);
        }

        if (collider.gameObject.tag == "teleport")
        {
            gameObject.transform.position = respawn.transform.position;
        }
    }

    protected override void Die()
    {
        Destroy(gameObject);
        diePanel.gameObject.SetActive(true);
        UnityEngine.Cursor.visible = true;
    }

    public void Cont()
    {
        pause.SetActive(false);
    }

}


public enum CharState
{
    Idle,
    Run,
    Jump,
    Squat
}
