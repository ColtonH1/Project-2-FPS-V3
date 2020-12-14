using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;

    Transform target;
    NavMeshAgent agent;

    public int currentHealth = 3;
    private AudioSource shootAudio;
    public AudioClip impactClip;
    public AudioClip fireClip;
    public static int score = 0;

    //shooting
    private float timeBtwnShots;
    public float startTimeBtwnShots;

    public GameObject projectile1;
    //public GameObject projectile2;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        shootAudio = GetComponent<AudioSource>();
        timeBtwnShots = startTimeBtwnShots;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            Shoot();
            //HoldNavAgent();
            agent.SetDestination(target.position);
            Shoot();

            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
            }


        }
    }

    private void Shoot()
    {
        if (timeBtwnShots <= 0)
        {
            Instantiate(projectile1, transform.position, Quaternion.identity);
            //Instantiate(projectile2, transform.position, Quaternion.identity);
            shootAudio.PlayOneShot(fireClip);
            timeBtwnShots = startTimeBtwnShots;
        }
        else
        {
            timeBtwnShots -= Time.deltaTime;
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    public void Damage(int damageAmount)
    {
        score += 5;
        currentHealth -= damageAmount;
        shootAudio.PlayOneShot(impactClip);
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public static bool isPlayerDead;
    public static bool GetPlayerIsDead()
    {
        Debug.Log("Static Bool, player is " + Player.IsPlayerDead());
        return Player.IsPlayerDead();
    }

    public static void ResetScore()
    {
        score = 0;
    }
    public static int GetScore()
    {
        return score;

    }

    /*public IEnumerator HoldNavAgent()
    {
        yield return new WaitForSeconds(0.1f);
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;
        //target = PlayerManager.instance.player.transform;
        //agent.speed = 2;
        agent.SetDestination(target.transform.position);
    }*/
}
