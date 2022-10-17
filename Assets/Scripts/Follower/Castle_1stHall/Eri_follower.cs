using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eri_follower : Follower
{
    public Conversation convo;
    private Animator animator;
    private static bool isFollowing;
    public VectorValue startingPosition;

    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);  
    }

    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        Trace();
        TryInteract();
    }

    void FixedUpdate() 
    {
        if (!FollowingManager.isFollowing)
        return;

        if (playerInRange)
        {
            animator.SetBool("moving", false);
            FollowingManager.isFollowing = false;
            DialogueManager.StartConversation(convo);
            return;
        }

        Move();
        Change();
    }

    protected void Initialize() {
        rb = this.GetComponent<Rigidbody2D>();
        boxCollider = this.GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
        if (FollowingManager.isFollowing)
        {
            FollowingManager.instance.SwitchScene(startingPosition.initialValue);
        }
    }

    void Change() 
    {
        float x = movement.x;
        float y = movement.y;

        if (animator == null) 
        return;
        
        if (this.direction == Direction.Horizontal) 
        {
            if (Mathf.Abs(x) < 0.01f)
            return;
            animator.SetFloat("moveX", x);
            animator.SetFloat("moveY", 0);
            animator.SetBool("moving", true);
        } 
        else 
        {
            if (Mathf.Abs(y) < 0.01f)
            return;
            animator.SetFloat("moveY", y);
            animator.SetFloat("moveX", 0);
            animator.SetBool("moving", true);
        }
    }
}
