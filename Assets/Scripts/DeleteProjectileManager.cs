using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteProjectileManager : MonoBehaviour
{
    public float timeBeforeDeleting;

    void Update()
    {
        timeBeforeDeleting -= Time.deltaTime;
        if(timeBeforeDeleting<=0f)
        {
            Destroy(gameObject);
            timeBeforeDeleting=1f;
        }
    }
}
