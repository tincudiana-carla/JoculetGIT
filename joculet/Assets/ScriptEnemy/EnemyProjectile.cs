using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    [SerializeField] private float lifeTime;
    public void ActivateProjectTile()
    {
        lifeTime = 0;
        gameObject.SetActive(true);
    }
    private void Update()
    {
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);
        lifeTime += Time.deltaTime;
        if (lifeTime > resetTime)
            gameObject.SetActive(false);

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision); // go to the enemyscript(parent script)
        gameObject.SetActive(false);
    }

}
