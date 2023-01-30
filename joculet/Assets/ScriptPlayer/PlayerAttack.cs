
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    private PlayerMove playerMovement;
    [SerializeField] private float attackCooldown;
    [SerializeField] public Transform bulletPoint;
    [SerializeField] public GameObject[] Bullets;
    
    private float cooldownTimer = Mathf.Infinity;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMove>();
    }
    private void Update()
    {
        if(Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
        {
            Attack();
        }
        cooldownTimer += Time.deltaTime;
    }
    private void Attack()
    {
        anim.SetTrigger("Attack");
        cooldownTimer = 0;
        Bullets[FindBullet()].transform.position = bulletPoint.position;
        Bullets[FindBullet()].GetComponent<ProjectTile>().setDirection(Mathf.Sign(transform.localScale.x));
    }
    private int FindBullet()
    {
        for(int i = 0; i<Bullets.Length;i++)
        {
            if (!Bullets[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
  

}
