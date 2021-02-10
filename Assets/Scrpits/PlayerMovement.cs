using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerState {
    walk,
    attack,
    interact,
    stagger,
    idle
}
public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;
    public FloatValue currentHealth;
    public Signal playerHealthSignal;
    public inventory inventory;
    public SpriteRenderer receivedItemSprite;
    public bool updateAnimationAndMove;
    public Signal playerHit;
   

    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        animator.SetFloat("MoveX", 0);
        animator.SetFloat("MoveY", -1);
        
    }

    // Update is called once per frame
    

    void FixedUpdate() {
       if (updateAnimationAndMove) {
            UpdateAnimationAndMove();
            updateAnimationAndMove = false;
        }
}
    void Update() {
        if (currentState == PlayerState.interact) {
            return;
        }
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger) {
            StartCoroutine(AttackCo());
        }
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle) {
            updateAnimationAndMove = true;
        }

    }

        private IEnumerator AttackCo() {
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.2f);
        if(currentState != PlayerState.interact) {
            currentState = PlayerState.walk;
        }
    }
    public void RaiseItem() {
        if(inventory.currentItem != null) {
            if(currentState != PlayerState.interact) {
            //animator.SetBool("", true);
            currentState = PlayerState.interact;
            receivedItemSprite.sprite = inventory.currentItem.itemSprite;
            }
        else {
            currentState = PlayerState.idle;
            receivedItemSprite.sprite = null;
            inventory.currentItem = null;
            }
        }
    }
    void UpdateAnimationAndMove() {
        if (change != Vector3.zero) {
            MoveCharacter();
            animator.SetFloat("MoveX", change.x);
            animator.SetFloat("MoveY", change.y);
            animator.SetBool("moving", true);
        }

        else {
            animator.SetBool("moving", false);
        }
    }
    void MoveCharacter() {
        change.Normalize();
        myRigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
    }
    private IEnumerator KnockCo( float knockTime) {
        playerHit.Raise();
        if (myRigidbody != null) {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }
    public void Knock(float knockTime, float damage) {
        currentHealth.RunTimeValue -= damage;
        if (currentHealth.RunTimeValue > 0) {
            playerHealthSignal.Raise();
            StartCoroutine(KnockCo(knockTime));
        }
        else {
                this.gameObject.SetActive(false);
           
        }
    }
}
