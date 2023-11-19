using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class TowerManager : Loader<TowerManager>
{
    private TowerButton towerButtonPressed;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))  //М: 0 - нажатие ЛКМ
        {
            Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePoint, Vector2.zero);

            if (hit.collider.tag == "TowerSide")
            {
                hit.collider.tag = "TowerSideFull";

                PlaceTower(hit);
            }
        }

        if (spriteRenderer.enabled)
        {
            FollowMouse();
        }
    }

    public void PlaceTower(RaycastHit2D hit)
    {
        if (!EventSystem.current.IsPointerOverGameObject() && towerButtonPressed != null)
        {
            GameObject newTower = Instantiate(towerButtonPressed.TowerObject);
            newTower.transform.position = hit.transform.position;
            DisableDrag();
        }
    }

    public void SelectedTower(TowerButton towerSelected)
    {
        towerButtonPressed = towerSelected;
        EnableDrag(towerButtonPressed.DragSprite);

        Debug.Log("Pressed" + towerButtonPressed.gameObject);
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
