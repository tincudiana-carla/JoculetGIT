using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth;
    private bool dead;
    private Animator anim;
    [Header("Frames")]
    [SerializeField] private float iFramesDurations;
    [SerializeField] public int numberOffFlashes;
    [SerializeField] public SpriteRenderer spriteRend;
    public static event Action OnPlayerDeath;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        if(currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());
            
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");
                GetComponent<PlayerMove>().enabled = false;
                dead = true;
                StopCoroutine(Invunerability());
                FindObjectOfType<GameManager>().EndGame();
            }
            OnPlayerDeath?.Invoke();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            TakeDamage(1);
    }
    public void AddHealth( float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(9, 10, true);
        for (int i = 0; i < numberOffFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5F);
            yield return new WaitForSeconds(iFramesDurations / (numberOffFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDurations / (numberOffFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(9, 10, false);
    }
}
