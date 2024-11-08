using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Weapon weaponHolder; // The player's weapon holder
    [SerializeField] private Weapon weapon;       // The weapon to be picked up

    [SerializeField] private SpriteRenderer weaponSpriteRenderer; // The SpriteRenderer component
    [SerializeField] private Animator weaponAnimator;            // The Animator component

    private void Awake()
    {
        if (weapon != null)
        {
            // Hide the weapon visuals initially
            TurnVisual(false);
        }
    }

    private void Start()
    {
        // Ensure that visuals are turned off initially
        TurnVisual(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            // Equip the weapon to the player's weapon holder
            if (weaponHolder != null && weapon != null)
            {
                AssignWeaponToHolder();
                TurnVisual(true);

                // Destroy the pickup object after collecting the weapon
                Destroy(gameObject);
            }
        }
    }

    // Method to enable or disable weapon visuals
    private void TurnVisual(bool isActive)
    {
        if (weaponSpriteRenderer != null)
            weaponSpriteRenderer.enabled = isActive;

        if (weaponAnimator != null)
            weaponAnimator.enabled = isActive;
    }

    // Method to assign the weapon's sprite and animation to the weapon holder
    private void AssignWeaponToHolder()
    {
        // Copy the sprite to the player's weapon holder
        SpriteRenderer holderSpriteRenderer = weaponHolder.GetComponent<SpriteRenderer>();
        if (holderSpriteRenderer != null && weaponSpriteRenderer != null)
        {
            holderSpriteRenderer.sprite = weaponSpriteRenderer.sprite;
        }

        // Copy the animation controller to the player's weapon holder
        Animator holderAnimator = weaponHolder.GetComponent<Animator>();
        if (holderAnimator != null && weaponAnimator != null)
        {
            holderAnimator.runtimeAnimatorController = weaponAnimator.runtimeAnimatorController;
        }
    }
}



