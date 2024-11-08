using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    PlayerMovement playerMovement;
    Animator animator;

    //singleton untuk memastikan hanya ada satu objek saja yang ada 
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != null)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        
        GameObject engineeffect = GameObject.Find("EngineEffect");

        animator = engineeffect.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerMovement.Move();
    }

    void LateUpdate()
    {
        if(animator != null)
        {
            animator.SetBool("IsMoving",playerMovement.IsMoving());
        }
        
    }
}