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
    public ParticleSystem dust;
    public PlayerState currentState;
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;
    public FloatValue currentHealth;
    public Signal playerHealthSignal;
    public inventory inventory;
    public SpriteRenderer receivedItemSprite;
    
    public Signal playerHit;
    public bool inCombat;
    private float outOfCombatTimer;
    private float outOfCombatDelay;
    [SerializeField] public FloatValue regenAmount;



    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        animator.SetFloat("MoveX", 0);
        animator.SetFloat("MoveY", -1);
        DistableDust();

    }

    // Update is called once per frame
    

    void FixedUpdate() {

        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if(currentState == PlayerState.walk || currentState == PlayerState.idle) {
            UpdateAnimationAndMove();

        }
        
    }
    void Update() {
       
        if (currentState == PlayerState.interact) {  
            return;
        }
        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger) {
            StartCoroutine(AttackCo());
            


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
            CreatDust();
            

        }

        else {
            
            animator.SetBool("moving", false);
            DistableDust();

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
    void CreatDust() {
        if (!dust.isPlaying) {
            dust.Play();
        }
    }
    void DistableDust() {
        
        if (dust.isPlaying) {
            dust.Stop();
        }
    }
}
