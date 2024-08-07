using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.UI;

using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class AI8 : MonoBehaviour
{
    private Animator anim;
    public float maxHealth = 500;
    float currentHealth;
    private float attackTimer2 = 0f;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackRange = 0.7f;
    public float attackDamage = 7;
    public float BaseMoveSpeed = 1.6f;
    public float TemporaryMovementSpeed = 0.1f;
    public float attackCooldown = 2f;
    private Transform target;
    public GameObject bullet;
    public Transform firePoint;
    public float bulletSpeed = 6f;
    public float bulletfiretime = 3f;
    private Rigidbody2D rb2d;
    public float bulletattackTimer = 3f;
    private float attackTimer = 5f;
    private float timer = 2;
    private float timer2 = 2;
    float distance;
    public float enemyscalex = 0.19f;
    Vector2 scaler;
    private bool isShieldActive = false;
    private float activationTimer = 0f;
    public float secondskillbutton = 15f;
    private float shieldTimer = 0f;
    private float speedBoostTimer = 0f;
    private bool isSpeedBoosted = false;
    private bool solagitme = false;
    private bool sagagitme = false;
    private bool ortayagitme = false;

    float step;
    public Image HealthBar;
    public AudioSource muzikbutton1;
    public AudioSource muzikbutton2;

    public GameObject youwin;

    void Start()
    {
        muzikbutton1.Stop();
        muzikbutton2.Stop();
        rb2d = GetComponent<Rigidbody2D>();
        Physics2D.queriesStartInColliders = false;
        currentHealth = maxHealth;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        youwin.SetActive(false);
        Time.timeScale = 1;


    }
    void Update()
    {
        anim = GetComponent<Animator>();
        Vector2 scaler = transform.localScale;
        AttackCharacter();
        MoveTowardsPlayer();
        attackTimer2 -= Time.deltaTime;
        distance = rb2d.position.x - target.position.x;
        EfirstSkillShoot();
        EnemyFlip();
        ESEcondSkillShoot();
        getSlowed();
    }
    public void hizskill()
    {
        if (!isSpeedBoosted)
        {
            isSpeedBoosted = true;
            speedBoostTimer = 0f;
            BaseMoveSpeed = 3f;
            attackTimer = 5f;
            bulletattackTimer += 4f;
        }
    }
    public void bearskill()
    {
        if (!isSpeedBoosted)
        {
            isSpeedBoosted = true;
            speedBoostTimer = 0f;
            BaseMoveSpeed = 0f;
            attackTimer = 5f;
            bulletattackTimer += 4f;
        }
    }
    private void ResetSpeed()
    {
        isSpeedBoosted = false;
        BaseMoveSpeed = 1.6f;
    }
    public void getSlowed()
    {

        if (isSpeedBoosted)
        {
            speedBoostTimer += Time.deltaTime;

            if (speedBoostTimer >= 3)
            {
                // H�z art��� s�resi doldu�unda normal h�za d�n
                ResetSpeed();
            }
        }
    }
    private void DisableShield()
    {
        // Kalkan� devre d��� b�rak
        isShieldActive = false;
    }
    private void EnableShield()
    {
        muzikbutton2.Play();
        anim.SetTrigger("Enemysecondskill8");

        // Kalkan� etkinle�tir
        isShieldActive = true;
        shieldTimer = 0f;
    }
    public void TakeDamage(float damage)
    {
        if (isShieldActive)
        {
            Debug.Log("Zarar tamamen korundu");
        }
        else if (attackTimer2 <= 0)
        {
            attackTimer2 = 5;
            Debug.Log("D��man hasar� engellendi");
            anim.SetTrigger("Enemydefence8");
            damage -= 4;
            currentHealth -= damage;
        }
        else
        {
            currentHealth -= damage;
            Debug.Log("d��man hasar�");
        }
        HealthBar.fillAmount = currentHealth / 500; 

        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            

            Time.timeScale = 0;
            youwin.SetActive(true);

            int currentGold = PlayerPrefs.GetInt("GoldAmount");
            currentGold += 350;
            PlayerPrefs.SetInt("GoldAmount", currentGold);
        
        }
        
    }

    void EnemyFlip()
    {
        if (distance < 0)
        {
            scaler = transform.localScale;
            scaler.x = enemyscalex;
            transform.localScale = scaler;
        }
        else
        {
            scaler = transform.localScale;
            scaler.x = -enemyscalex;
            transform.localScale = scaler;
        }
    }
    void MoveTowardsPlayer()
    {
        float newX;
        step = BaseMoveSpeed * Time.deltaTime;


        timer -= Time.deltaTime;
        if (timer <= 0)
        {

            step = 0;
            timer2 -= Time.deltaTime;
        }
        if (timer2 <= 0)
        {
            timer = Random.Range(3, 8);
            timer2 = Random.Range(1, 3);
        }



        if (distance >= 3)
        {
            solagitme = true;
            sagagitme = false;
        }
        if (distance > -3 && solagitme == true)
        {
            newX = Mathf.MoveTowards(transform.position.x, target.position.x - 3f, step);

        }
        else if (distance > -3 && distance < 3 && sagagitme == true)
        {
            newX = Mathf.MoveTowards(transform.position.x, target.position.x + 3f, step);

        }
        else
        {
            newX = Mathf.MoveTowards(transform.position.x, target.position.x + 3f, step);
            sagagitme = true;
            solagitme = false;
        }
        if (transform.position.x < -9f || ortayagitme == true)
        {
            newX = Mathf.MoveTowards(transform.position.x, 0, step);
            ortayagitme = true;
        }
        else if(transform.position.x > 9f || ortayagitme == true)
        {
            newX = Mathf.MoveTowards(transform.position.x, 0, step);
            ortayagitme = true;
        }
        if (transform.position.x < 2 && transform.position.x > -2)
        {
            ortayagitme = false;
        }

        Vector3 newPosition = new Vector3(newX, transform.position.y, transform.position.z);

        transform.position = newPosition;
        if(step==0){
            anim.SetBool("Enemywalk8",false);
        }
        else{
            anim.SetBool("Enemywalk8",true);
        }

    }

    void AttackCharacter()
    {

        if (attackTimer <= 0){
            if (distance < 1 && distance > -1)
            {
                
                attackDamage = 7;
                AttackPlayer();
                anim.SetTrigger("Enemymelee8");
                attackTimer = attackCooldown;
            }   
         }
         else{
            attackTimer -= Time.deltaTime;
         }
    }

    void AttackPlayer()
    {
            Collider2D[] hitCharacter = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitCharacter)
            {
                Debug.Log("Zarar" + enemy.name);

                if (enemy.name == "1(Clone)")
                {
                    enemy.GetComponent<Char1>().CharacterTakeDamage(attackDamage);
                }
                else if (enemy.name == "2(Clone)")
                {
                    enemy.GetComponent<Char2>().CharacterTakeDamage(attackDamage);
                }
                else if (enemy.name == "3(Clone)")
                {
                    enemy.GetComponent<Char3>().CharacterTakeDamage(attackDamage);
                }
                else if (enemy.name == "4(Clone)")
                {
                    enemy.GetComponent<Char4>().CharacterTakeDamage(attackDamage);
                }
                else if (enemy.name == "5(Clone)")
                {
                    enemy.GetComponent<Char5>().CharacterTakeDamage(attackDamage);
                }
                else if (enemy.name == "6(Clone)")
                {
                    enemy.GetComponent<Char6>().CharacterTakeDamage(attackDamage);
                }
                else if (enemy.name == "7(Clone)")
                {
                    enemy.GetComponent<Char7>().CharacterTakeDamage(attackDamage);
                }
                else if (enemy.name == "8(Clone)")
                {
                    enemy.GetComponent<Char8>().CharacterTakeDamage(attackDamage);
                }
                else if (enemy.name == "9(Clone)")
                {
                    enemy.GetComponent<Char9>().CharacterTakeDamage(attackDamage);
                }
                else if (enemy.name == "10(Clone)")
                {
                    enemy.GetComponent<Char10>().CharacterTakeDamage(attackDamage);
                }
                else if (enemy.name == "11(Clone)")
                {
                    enemy.GetComponent<Char11>().CharacterTakeDamage(attackDamage);
                }
                else if (enemy.name == "12(Clone)")
                {
                    enemy.GetComponent<Char12>().CharacterTakeDamage(attackDamage);
                }
                else if (enemy.name == "13(Clone)")
                {
                    enemy.GetComponent<Char13>().CharacterTakeDamage(attackDamage);
                }
                else if (enemy.name == "14(Clone)")
                {
                    enemy.GetComponent<Char14>().CharacterTakeDamage(attackDamage);
                }
            }
           

    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    void EfirstSkillShoot()
    {
        if (distance > 1.2 || distance < -1.2)
        {
            if (bulletattackTimer <= 0)
            {
                GameObject mermi = Instantiate(bullet, firePoint.position, firePoint.rotation);
                muzikbutton1.Play();
                anim.SetTrigger("Enemyfirstskill8");
                if (transform.localScale.x >= 0f)
                {
                    Rigidbody2D rb = mermi.GetComponent<Rigidbody2D>();
                   Vector2 launchDirection = new Vector2(0.6f, 0.2f);
                    rb.velocity = launchDirection * bulletSpeed;
                    bulletattackTimer = bulletfiretime;

                    scaler = rb.transform.localScale;
                    scaler.x = 0.5f;
                    rb.transform.localScale = scaler;

                }
                else
                {
                    Rigidbody2D rb = mermi.GetComponent<Rigidbody2D>();
                    Vector2 launchDirection = new Vector2(-0.6f, 0.2f);
                    rb.velocity = launchDirection * bulletSpeed;
                    bulletattackTimer = bulletfiretime;

                    scaler = rb.transform.localScale;
                    scaler.x = -0.5f;
                    rb.transform.localScale = scaler;
                }
            }
            else
            {
                bulletattackTimer -= Time.deltaTime;
            }
        }
    }
    void ESEcondSkillShoot()
    {
        if (!isShieldActive)
        {
            activationTimer += Time.deltaTime;

            if (activationTimer >= secondskillbutton)

            {
                anim.SetTrigger("Enemysecondskill8");
                EnableShield();
                activationTimer = 0f; // Etkinle�tirme aral��� s�f�rlan�r
            }
        }

        if (isShieldActive)
        {
            shieldTimer += Time.deltaTime;

            if (shieldTimer >= 3)
            {
                // Kalkan s�resi doldu�unda kalkan� devre d��� b�rak
                DisableShield();
            }
        }
    }
}




