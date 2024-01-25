using UnityEngine;

public class TowerButton : MonoBehaviour
{
    [SerializeField]
    private readonly TowerControll towerObject;

    [SerializeField]
    private readonly Sprite dragSprite;

    [SerializeField]
    private readonly int towerPrice;

    public TowerControll TowerObject => towerObject;

    public Sprite DragSprite => dragSprite;

    public int TowerPrice => towerPrice;
}
