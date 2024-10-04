using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public string targetTag = "Enemy"; // Tag of the objects you want to destroy
    public SpriteRenderer attackSprite;
    public Collider2D attackRange; // Assign the attack range collider here
    private GameObject targetObject; // Store reference to the detected target

    private void Start()
    {
        if (attackSprite == null)
        {
            Debug.LogError("Attrack Sprite not assigned!");
        }
        if (attackRange == null)
        {
            Debug.LogError("Attack range collider not assigned!");
        }
    }

    private void Update()
    {
        // Check if player presses the attack button (e.g., left mouse click or space bar)
        if (Input.GetButtonDown("Fire1")) // "Fire1" is typically the left mouse button or Ctrl key
        {
            attackSprite.color = Color.red; // Change sprite color to red
            if (targetObject != null)
            {
                Destroy(targetObject); // Destroy the object within attack range
                targetObject = null; // Reset target reference after attack
            }
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            attackSprite.color = Color.white; // Reset sprite color
        }
    }

    // Detect when an object enters the player's attack range
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            targetObject = other.gameObject; // Store reference to the target object
        }
    }

    // Detect when an object exits the player's attack range
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            targetObject = null; // Remove reference when target exits range
        }
    }
}
