using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    Transform barrelPos;
    [SerializeField] public Transform bulletHolder;
    

    public void AC_shootBullet()
    {
        
        var b =Instantiate(bulletPrefab, barrelPos.position, Quaternion.identity);
        b.GetComponent<ProjectTile>().setDirection(transform.localScale.x);
        b.transform.SetParent(bulletHolder);
    }
}
