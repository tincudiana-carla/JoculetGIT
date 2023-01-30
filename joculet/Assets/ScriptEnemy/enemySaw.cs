using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySaw : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float movementDistance;
    private bool movingLeft;
    private float edgeLeft;
    private float edgeRight;
    private void Awake()
    {
        edgeLeft = transform.position.x - movementDistance;
        edgeRight = transform.position.x + movementDistance;

    }
    private void Update()
    {
        if(movingLeft)
        {
            if (transform.position.x > edgeLeft)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else movingLeft = false;
        }
        else
        {
            if (transform.position.x < edgeRight)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else movingLeft = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
