using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHead : EnemyDamage
{
    [Header("SpikeHead Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField]  private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    private float checkTime;
    private bool attacking;
    private Vector3[] directions = new Vector3[4];
    private Vector3 destination;

    private void OnEnable()
    {
        Stop();
    }
    private void Update()
    {
        //move spike to destination only if attacking
        if(attacking)
             transform.Translate(destination * Time.deltaTime * speed);
        else
        {
            checkTime += Time.deltaTime;
            if (checkTime > checkDelay)
                CheckForPlayer();
        }
    }
    private void CheckForPlayer()
    {
        CalculateDirection();
        //checking if the enemy sees player in all the 4 directions
        for(int i =0;i<directions.Length;i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);
            if(hit.collider != null && !attacking)
            {
                attacking = true;
                destination = directions[i];
                checkTime = 0;
            }
        }
    }
    private void CalculateDirection()
    {
        directions[0] = transform.right * range;
        directions[1] = -transform.right * range;
        directions[2] = transform.up * range;
        directions[3] = -transform.up * range;

    }
    private void Stop()
    {
        destination = transform.position;
        attacking = false;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        Stop();
    }

}
