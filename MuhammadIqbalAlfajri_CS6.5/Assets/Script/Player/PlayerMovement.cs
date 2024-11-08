using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Vector2 maxSpeed;
    [SerializeField] Vector2 timeToFullSpeed;
    [SerializeField] Vector2 timeToStop;
    [SerializeField] Vector2 stopClamp;
    Vector2 moveDirection;
    Vector2 moveVelocity;
    Vector2 moveFriction;
    Vector2 stopFriction;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        moveVelocity = 2 * maxSpeed/timeToFullSpeed;

        moveFriction = 2 * maxSpeed/(timeToFullSpeed*timeToFullSpeed);
        
        stopFriction = -2 * maxSpeed/(timeToStop*timeToStop);
    }

    // Update is called once per frame
    public void Move()
    {
        //movement control Using WASD each key move within X or Y axis
        float inputX = 0;
        float inputY = 0;

        if(Input.GetKey(KeyCode.W)){
            inputY += 1;
        }
        if(Input.GetKey(KeyCode.S)){
            inputY -= 1;
        }
        if(Input.GetKey(KeyCode.D)){
            inputX += 1;
        }
        if(Input.GetKey(KeyCode.A)){
            inputX -= 1;
        }

        moveDirection = new Vector2(inputX, inputY).normalized;

        Vector2 targetVelocity = moveDirection * maxSpeed;

        if(moveDirection != Vector2.zero){//vector2.zero == 0,0
            rb.velocity = Vector2.MoveTowards(rb.velocity, targetVelocity, moveVelocity.magnitude * Time.fixedDeltaTime);
        }
        else{
            rb.velocity = Vector2.MoveTowards(rb.velocity, Vector2.zero, stopFriction.magnitude * Time.fixedDeltaTime);
        }

        //limit rb velocity with Mathf.clamp
        rb.velocity = new Vector2(
            Mathf.Clamp(rb.velocity.x, -stopClamp.x, stopClamp.x), 
            Mathf.Clamp(rb.velocity.y, -stopClamp.y, stopClamp.y)
        );
    }

    //GetFriction
    public Vector2 GetFriction()
    {
        if(moveDirection != Vector2.zero){
            return moveFriction;
        }
        else{
            return stopFriction;
        }
        
    }

    public void MoveBound()
    {

    }

    public bool IsMoving()
    {
        return moveDirection != Vector2.zero;
    }
}