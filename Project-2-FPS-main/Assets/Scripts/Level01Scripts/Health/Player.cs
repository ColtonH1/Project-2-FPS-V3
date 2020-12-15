using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] Text _currentHealthTextView;
    public Camera cam;
    private AudioSource Impact;
    public AudioClip HurtSound;
    public AudioClip DeathSound;

    public Interactable focus;
    //PlayerMovement playerMovement;
    public int maxHealth = 100;
    public int currentHealth;
    public static int armor;
    public static int totalArmor;

    public HealthBar healthBar;



    //death
    public static bool playerIsDead = false;
    public GameObject deathMenuUI;
    public GameObject reticle;

    public Level01Controller level01Controller;
    

    

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        _currentHealthTextView.text = "Health: " + currentHealth.ToString();
        Impact = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TakeDamage(20);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            TakeDamage(-20);
        }*/

        totalArmor = armor;
        if (currentHealth == 0)
        {
            Die();
        }

        if (Input.GetMouseButton(2))
        {
            RemoveFocus();
        }

        if (Input.GetMouseButton(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if(Physics.Raycast(ray, out hit, 100))
            {
                Interactable interactable =  hit.collider.GetComponent<Interactable>();
                if(interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }
    }

    public static void AddArmor(int newArmor)
    {
        armor += newArmor;
    }

    public static void RemoveArmor(int oldArmor)
    {
        armor -= oldArmor;
    }

    private void SetFocus(Interactable newFocus )
    {
        if(newFocus != focus)
        {
            if(focus != null)
            {
                focus.OnDefocused();
            }
            focus = newFocus;
        }
        newFocus.OnFocused(transform);
        //playerMovement.FollowTarget(newFocus);
    }

    void RemoveFocus()
    {
        if (focus != null)
        {
            focus.OnDefocused();
        }
        focus = null;
        //playerMovement.StopFollowingTarget();
    }

    private void Die()
    {
        Impact.PlayOneShot(DeathSound);
        deathMenuUI.SetActive(true);
        reticle.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        playerIsDead = true;

    }

    void TakeDamage(int damage)
    {
        //armor
        damage -= armor;//.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        //clamp health
        Debug.Log("player take damage");
        currentHealth -= damage;
        if(currentHealth < 0)
        {
            currentHealth = 0;
        }
        if (currentHealth > 100)
        {
            currentHealth = 100;
        }

        //update healthbar/health point
        healthBar.SetHealth(currentHealth);
        _currentHealthTextView.text = "Health: " + currentHealth.ToString();
    }

    void Heal(int health)
    {
        health = Mathf.Clamp(health, 0, int.MaxValue);
        currentHealth += health;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        if (currentHealth > 100)
        {
            currentHealth = 100;
        }
        //update healthbar/health point
        healthBar.SetHealth(currentHealth);
        _currentHealthTextView.text = "Health: " + currentHealth.ToString();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Armor")
        {
            //armor += 2;
        }
        else if (collider.tag == "Health")
        {
            Heal(10);
        }
        else if(collider.tag == "Treasure")
        {
            level01Controller.AddToScore(10);
        }
        else
        {
            Impact.PlayOneShot(HurtSound);
            TakeDamage(5);
        }
    }

    public static bool IsPlayerDead()
    {
        return playerIsDead;
    }
}
