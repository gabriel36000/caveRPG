using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TreasureChest : Interactable
{
    public inventory inventory;
    public Item contents;
    public bool isOpen;
    public Signal raiseItem;
    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange) {
            if (!isOpen) {
                OpenChest();
            }
            else {
                ChestAlreadyOpen();
            }
        }
    }
    public void OpenChest() {
        dialogBox.SetActive(true);
        dialogText.text = contents.itemDescription;
        inventory.AddItem(contents);
        inventory.currentItem = contents;
        raiseItem.Raise();
        context.Raise();
        isOpen = true;
        anim.SetBool("opened", true);
        
    }
    public void ChestAlreadyOpen() {
        
            dialogBox.SetActive(false);
            
            raiseItem.Raise();
            
        
        
    }
    private void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player") && !col.isTrigger && !isOpen) {
            context.Raise();
            playerInRange = true;

        }
    }
    private void OnTriggerExit2D(Collider2D col) {
        if (col.CompareTag("Player") && !col.isTrigger && !isOpen) {
            context.Raise();
            playerInRange = false;

        }
    }
}
