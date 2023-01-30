using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTile : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool hit;
    private BoxCollider2D boxCollider;
    private Animator anim;
    private float direction;
    [SerializeField] Rigidbody2D rb;
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (hit) return; 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false ;
        anim.SetTrigger("explode");
        

    }
    public void setDirection(float _direction)
    {
        rb.velocity=Vector2.right*speed*_direction;

        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;
        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
        {
            localScaleX = -localScaleX;
        }
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    private void Deactivate()
    {
        
        gameObject.SetActive(false);        
    }
    private void Explode()
    {
        
    }


}
