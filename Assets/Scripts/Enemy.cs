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


    private int target = 0;  //М: номер точки MovePoint
    Transform enemy;
    private float navigationTime = 0;


    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Transform>();
        Manager.Instance.RegisterEnemy(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (wayPoints != null)
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
    }
}
