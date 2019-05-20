﻿using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : TargetTracker
{
    #region Variables
    [SerializeField]
    protected float slowDownDistance, slowSpeedMultiplier, followMaxDistance, damping, boundsOffset;
    #endregion

    private void Start()
    {
        moveSpeed = GetComponent<EntityBase>().stats.moveSpeed;
    }

    protected override void CalculateMovement()
    {
        if (rgbd.position.z <= GameController.Instance.bounds.zMin - boundsOffset)
        {
            Destroy(gameObject);
        }

        if (distanceToTarget <= followMaxDistance && distanceToTarget > slowDownDistance)
        {
            targetDir = targetRgbd.position - rgbd.position;
            newDir = Vector3.Lerp(Vector3.back, targetDir, damping * Time.deltaTime);
            targetPos = Vector3.MoveTowards(rgbd.position, targetRgbd.position, step * slowSpeedMultiplier);
        }
        
        else if (Vector3.Distance(rgbd.position, targetRgbd.position) > followMaxDistance)
        {
            Vector3 target = new Vector3(rgbd.position.x, rgbd.position.y, GameController.Instance.bounds.zMin - boundsOffset);
            targetDir = target - rgbd.position;
            newDir = Vector3.Lerp(Vector3.back, targetDir, damping * Time.deltaTime);
            targetPos = Vector3.MoveTowards(rgbd.position, target, step);
        }
    }
}


