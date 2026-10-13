using UnityEngine;

public class Paddle : MonoBehaviour
{
    public Rigidbody2D rb2D;
    public int id; 
    public float moveSpeed = 2f;
    public float aiDeadzone = 1f;

    private int direction = 0;
    private Vector3 startPosition;

    private float moveSpeedMultiplier = 1f;

    private void Start()
    {
        startPosition = transform.position;
        GameManager.instance.onReset += resetPosition;

    }

    private void resetPosition()
    {
        transform.position = startPosition;
    }

    public void Update()
    {
        if (id == 2 && GameManager.instance.IsPlayer2Ai()) 
        {
            MoveAi();
        }
        else
        {
            float movement = processInput();
            Move(movement);
        }


        
        
    }



    public void MoveAi()
    {
        Vector2 ballPos = GameManager.instance.ballMovement.transform.position;
        
        if(Mathf.Abs(ballPos.y - transform.position.y) > aiDeadzone)
        {
            direction = ballPos.y > transform.position.y ? 1 : -1;
        }

        if (Random.value < 0.01f)
        {
            moveSpeedMultiplier = Random.Range(0.5f, 1.3f);
        }

        Move(direction);
    }
    
    private float processInput()
    {
        float movement = 0f;
        switch (id)
        {
            case 1:
                movement = Input.GetAxis("MovePlayer1");
                break;
            case 2:
                movement = Input.GetAxis("MovePlayer2");
                break;
        }

        return movement;
    }

    private void Move(float movement)
    {
        Vector2 velo =rb2D.linearVelocity;
        velo.y = moveSpeed * moveSpeedMultiplier * movement ;
        rb2D.linearVelocity = velo ;
    }
}
