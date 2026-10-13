using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public Rigidbody2D rb2D;
    public BallAudio ballAudio;
    public ParticleSystem collisionParticle;

    public float maxInitialAngle = 0.67f;
    public float moveSpeed = 1f;
    public float maxStartY = 4f;
    public float speedMultiplier = 1.1f;
    
    private float startX = 0f;


    private void Start()
    {
        
        GameManager.instance.onReset += resetBall;
        GameManager.instance.gameUI.onStartGame += resetBall;
    }
    
    //resets ball and gives initial motion
    private void resetBall()
    {
        resetBallPosition();
        initialMotion();
    }

    private void initialMotion()
    {
        Vector2 dir = Random.value < 0.5? Vector2.left : Vector2.right; //random position on reset
        dir.y = Random.Range(-maxInitialAngle, maxInitialAngle); //random angle on reset
        rb2D.linearVelocity = dir * moveSpeed; //faster ball
        EmitParticle(32);
    }

    //reset ball after score
    private void resetBallPosition()
    {
        float posY =Random.Range(-maxStartY, maxStartY); //randomly on position on y-axis
        Vector2 position = new Vector2 (startX, posY); //after reset on fixed x-axis and random y-axis 
        transform.position = position; 
    }

   

    //difining scorezones and their actions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ScoreZone scoreZone = collision.GetComponent<ScoreZone>();//gets component by name
        if (scoreZone != null) //if collision with trigger occurs
        {
            GameManager.instance.onScoreZoneReached(scoreZone.id);
            

        }
    }

    //faster ball
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Paddle paddle = collision.collider.GetComponent<Paddle>(); //gets component collider by name
        if (paddle != null) //if collision wih paddle
        {
            ballAudio.PlayPaddleSound();
            rb2D.linearVelocity *= speedMultiplier; // multiplies velocity by a speed multiplier after each collison with paddle
            EmitParticle(16);
            GameManager.instance.screenShake.StartShake(Mathf.Sqrt(rb2D.linearVelocity.magnitude) * 0.02f, 0.075f);
        }


        WallCollision wall = collision.collider.GetComponent<WallCollision>(); //gets component collider by name
        if (wall != null) //if collision wih wall
        {
            ballAudio.PlayWallSound();
            rb2D.linearVelocity *= speedMultiplier;
            EmitParticle(8);
            GameManager.instance.screenShake.StartShake(0.033f, 0.033f);
        }
    }

    private void EmitParticle(int ammount)
    {
        collisionParticle.Emit(ammount);
    }
}
