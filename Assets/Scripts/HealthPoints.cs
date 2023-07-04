using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class HealthPoints : MonoBehaviour
{
    [SerializeField] private float health = 100f;

    private float x_Death = -90f;
    private float death_smooth = 0.9f;
    private float rotate_Time = 0.23f;
    private bool playerDied = false;

    public bool isPlayer;

    [SerializeField] private Image health_UI;
    private BearSoundFX soundFX;

    private PlayerInventory playerInventory; // Reference to PlayerInventory script

    void Awake()
    {
        soundFX = GetComponentInChildren<BearSoundFX>(); // because sound is a child of a bear 4 object
        playerInventory = GetComponent<PlayerInventory>(); // Get reference to PlayerInventory script
        playerInventory.OnHealthAdded += AddHealth; // Subscribe to the event for health update
    }
    

    void Update()
    {
        if (playerDied)
        {
            RotateAfterDeath();
        }
    }

    public void ApplyDamage(float damage)
    {
        health -= damage;

        if (health_UI != null)
        {
            health_UI.fillAmount = health / 100f; // Update health UI fill amount
        }

        if (health <= 0)
        {
            GetComponent<Animator>().enabled = false;

            StartCoroutine(AllowRotate());

            if (isPlayer)
            {
                GetComponent<MoveBear>().enabled = false;
                GetComponent<Attacks>().enabled = false;
                Camera.main.GetComponent<CameraControl>().PlayerDied();

                // The player is not the parent game object of the camera anymore
                Camera.main.transform.SetParent(null);

                GameObject.FindGameObjectWithTag(Tags.ENEMY_TAG).GetComponent<NPC_EnemyController>().enabled = false;
            }
            else
            {
                // GetComponent<Animator>().enabled = false;
                GetComponent<NPC_EnemyController>().enabled = false;
                GetComponent<NavMeshAgent>().enabled = false;
            }
        }
    }

    private void RotateAfterDeath()
    {
        transform.eulerAngles = new Vector3(Mathf.Lerp(transform.eulerAngles.x, x_Death, Time.deltaTime * death_smooth), transform.eulerAngles.y, transform.eulerAngles.z);
    }

    IEnumerator AllowRotate()
    {
        playerDied = true;

        yield return new WaitForSeconds(rotate_Time);

        playerDied = false;
    }

    // Method to handle health increase when mushrooms are collected
    public void AddHealth(int amount)
    {
        health += amount;

        if (health > 100f)
        {
            health = 100f;
        }

        if (health_UI != null)
        {
            health_UI.fillAmount = health / 100f; // Update health UI fill amount
        }
    }
    
}
