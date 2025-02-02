﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Enemies;
using UnityEngine;
using Random = UnityEngine.Random;

public class T4Spawn_Behavior : Base_Enemy_Behavior
{
    private T4SpawnEnemy values;
    public static string BULLET_NAME = "T4SpawnBullet";

    public override void MovementUpdate()
    {
        Vector3 toPos = Vector3.MoveTowards(transform.position, nextWaypoint, moveSpeed * Time.deltaTime);
        transform.position = toPos;
        
        if (Vector3.Distance(transform.position, nextWaypoint) < 1)
        {
            if (currentWaypointIndex != Waypoints.Count-1)
            {
                SetToNextWaypoint();
            }
        }
    }


    public override void SetupEnemy()
    {
        values = GameObject.Find("Model").GetComponent<T4SpawnEnemy>();
        shootInterval = values.fireRate / gameModel.fireRateMultiplier;
        shootTimer = Random.Range(0, shootInterval / 2);
        hitPoints = (int)(values.hp * gameModel.healthMultiplier);
        moveSpeed = values.moveSpeed * gameModel.speedMultiplier;
        bulletSpeed = values.bulletSpeed * gameModel.bulletSpeedMultiplier;
    }

    public override bool Immune()
    {
        return false;
    }

    public override bool canShoot()
    {
        if (inScreen())
        {
            return true;
        }

        return false;
    }

    public override void UpdateVisuals()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * rate);
    }

    public override void FiringPattern()
    {
        bullets.FireBullet(transform.position, (playerModel.ship.transform.position - transform.position).normalized , BULLET_NAME, this);
    }
    
    public override void KillThisEnemy()
    {
        effects.MakeExplosion(transform.position);
        gameModel.enemiesKilled++;
        playerModel.score += 1000;
        gameObject.SetActive(false);
    }

}