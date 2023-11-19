using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : Loader<TowerManager>
{
    TowerButton towerButtonPressed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectedTower(TowerButton towerSelected)
    {
        towerButtonPressed = towerSelected;
        Debug.Log("Pressed" + towerButtonPressed.gameObject);
    }
}
