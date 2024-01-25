using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private readonly Transform exit;  //М: точка финиша

    [SerializeField]
    private readonly Transform[] wayPoints;

    [SerializeField]
    private readonly float navigation;

    [SerializeField]
    private int health;

    [SerializeField]
    private readonly int rewardAmount;

    private int target = 0;  //М: номер точки MovePoint
    private Transform enemy;
    private Collider2D enemyCollider;
    private Animator anim;
    private float navigationTime = 0;
    private bool isDead = false;

    public bool IsDead => isDead;

    public void EnemyHit(int hitPoints)
    {
        if (health - hitPoints > 0)
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
        enemyCollider.enabled = false;
        Manager.Instance.TotalKilled += 1;
        Manager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.Death);
        Manager.Instance.addMoney(rewardAmount);
        Manager.Instance.IsWaveOver();
    }
}
