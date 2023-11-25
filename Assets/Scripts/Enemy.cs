using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Transform exit;  //М: точка финиша

    [SerializeField]
    private Transform[] wayPoints;

    [SerializeField]
    private float navigation;

    [SerializeField]
    int health;


    private int target = 0;  //М: номер точки MovePoint
    Transform enemy;
    Collider2D enemyCollider;
    Animator anim;
    private float navigationTime = 0;
    bool isDead = false;

    public bool IsDead
    {
        get{return  isDead;}
    }


    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Transform>();
        enemyCollider = GetComponent<Collider2D>();
        Manager.Instance.RegisterEnemy(this);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (wayPoints != null && isDead == false)
        {
            navigationTime += Time.deltaTime;
            if (navigationTime > navigation)
            {
               if (target < wayPoints.Length)
               {
                    enemy.position = Vector2.MoveTowards(enemy.position, wayPoints[target].position, navigationTime);
               }
               else
               {
                enemy.position = Vector2.MoveTowards(enemy.position, exit.position, navigationTime);
               }
               navigationTime = 0;
            }
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "MovingPoint")
        {
            target += 1;
        }
        else if (collision.tag == "Finish")
        {
            Manager.Instance.UnregisterEnemy(this);
        }
        else if (collision.tag == "projectile")
        {
            Projectile newP = collision.gameObject.GetComponent<Projectile>();
            EnemyHit(newP.AttackDamage);
            Destroy(collision.gameObject);
        }
    }

    public void EnemyHit(int hitPoints)
    {
        if(health - hitPoints>0)
        {
            health -= hitPoints;
            anim.Play("Uron");
        }
        else
        {
            anim.SetTrigger("didDie");
            Die();
        }
    }

    public void Die()
    {
        isDead = true;
    }
}
