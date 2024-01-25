using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerManager : Loader<TowerManager>
{
    public TowerButton towerButtonPressed { get; set; }
    private SpriteRenderer spriteRenderer;
    private readonly List<TowerControll> TowerList = new List<TowerControll>();
    private readonly List<Collider2D> BuildList = new List<Collider2D>();
    private Collider2D buildTile;

    public void RegisterBuildSite(Collider2D buildTag)
    {
        BuildList.Add(buildTag);
    }

    public void RegisterTower(TowerControll tower)
    {
        TowerList.Add(tower);
    }
    public void RenameTagBuildSite()
    {
        foreach (var buildTag in BuildList)
        {
            buildTag.tag = "TowerSide";
        }

        BuildList.Clear();
    }

    public void DestroyAllTowers()
    {
        foreach (var tower in TowerList)
        {
            Destroy(tower.gameObject);
        }

        TowerList.Clear();
    }

    public void PlaceTower(RaycastHit2D hit)
    {
        if (!EventSystem.current.IsPointerOverGameObject() && towerButtonPressed != null)
        {
            var newTower = Instantiate(towerButtonPressed.TowerObject);
            newTower.transform.position = hit.transform.position;
            BuyTower(towerButtonPressed.TowerPrice);
            Manager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.Build);
            RegisterTower(newTower);
            DisableDrag();
            towerButtonPressed = null;
        }
    }

    public void BuyTower(int price)
    {

        Manager.Instance.subtractMoney(price);
    }

    public void SelectedTower(TowerButton towerSelected)
    {
        if (towerSelected.TowerPrice <= Manager.Instance.TotalMoney)
        {
            towerButtonPressed = towerSelected;
            EnableDrag(towerButtonPressed.DragSprite);
        }
    }

    public void FollowMouse()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(transform.position.x, transform.position.y);
    }

    public void EnableDrag(Sprite sprite)  //М: хаха, drug
    {
        spriteRenderer.enabled = true;
        spriteRenderer.sprite = sprite;
    }

    public void DisableDrag()
    {
        spriteRenderer.enabled = false;
    }
}
