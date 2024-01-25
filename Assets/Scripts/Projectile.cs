using UnityEngine;

public enum projectileType
{
    lightning, flame
};

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private readonly int attackDamage;

    [SerializeField]
    private readonly projectileType pType;

    public int AttackDamage => attackDamage;

    public projectileType PType => pType;
}
