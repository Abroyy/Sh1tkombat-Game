using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.UI;

using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class AI12 : MonoBehaviour
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
    public GameObject bullet2;
    public Transform firePoint;
    public float bulletSpeed = 4f;
    public float bulletSpeed2 = 8f;
    public float bulletfiretime = 3f;
    public float secondskillcooldawn = 18f;
    private Rigidbody2D rb2d;
    public float bulletattackTimer = 3f;
    private float bulletattackTimer2 = 15f;
    private float attackTimer = 5f;
    private float timer = 2;
    private float timer2 = 2;
    float distance;
    public float enemyscalex = 0.2f;
    Vector2 scaler;
    private float speedBoostTimer = 0f;
    private bool isSpeedBoosted = false;
    private bool solagitme = false;
    private bool sagagitme = false;
    private bool ortayagitme = false;

    private float a = 0.35f;
    private float b = 0.7f;
    private float c = 1.05f;
    private float d = 1.4f;
    private float e = 0f;
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
        EsecondSkillShoot();
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
            bulletattackTimer2 += 4f;
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
            bulletattackTimer2 += 4f;
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
    public void TakeDamage(float damage)
    {
        if (attackTimer2 <= 0)
        {
            attackTimer2 = 5;
            Debug.Log("D��man hasar� engellendi");
            anim.SetTrigger("Enemydefence12");
            damage -= 4;
            currentHealth -= damage;
        }
        else
        {
            currentHealth -= damage;
            Debug.Log("d��man hasar�");
        }

        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            

           Time.timeScale = 0;
            youwin.SetActive(true);

            int currentGold = PlayerPrefs.GetInt("GoldAmount");
            currentGold += 350;
            PlayerPrefs.SetInt("GoldAmount", currentGold);
        
        }
        HealthBar.fillAmount = currentHealth / 500; 

        
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
            anim.SetBool("Enemywalk12",false);
        }
        else{
            anim.SetBool("Enemywalk12",true);
        }

    }

    void AttackCharacter()
    {

        if (attackTimer <= 0){
            if (distance < 1 && distance > -1)
            {
                
                attackDamage = 7;
                AttackPlayer();
                anim.SetTrigger("Enemymelee12");
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
                anim.SetTrigger("Enemyfirstskill12");
                if (transform.localScale.x >= 0f)
                {
                    Rigidbody2D rb = mermi.GetComponent<Rigidbody2D>();
                    Vector2 launchDirection = new Vector2(0.6f, 0.2f);
                    rb.velocity = launchDirection * bulletSpeed;
                    bulletattackTimer = bulletfiretime;

                    scaler = rb.transform.localScale;
                    scaler.x = 0.3f;
                    rb.transform.localScale = scaler;

                }
                else
                {
                    Rigidbody2D rb = mermi.GetComponent<Rigidbody2D>();
                    Vector2 launchDirection = new Vector2(-0.6f, 0.2f);
                    rb.velocity = launchDirection * bulletSpeed;
                    bulletattackTimer = bulletfiretime;

                    scaler = rb.transform.localScale;
                    scaler.x = -0.3f;
                    rb.transform.localScale = scaler;
                }
            }
            else
            {
                bulletattackTimer -= Time.deltaTime;
            }
        }
    }
    void EsecondSkillShoot()
    {
        if (bulletattackTimer2 <= 0)
        {
            anim.SetTrigger("Enemysecondskill12");
            muzikbutton2.Play();
            a -= Time.deltaTime;
            b -= Time.deltaTime;
            c -= Time.deltaTime;
            d -= Time.deltaTime;

            if (scaler.x <= 0)
            {
                if (scaler.x <= 0 && e <= 0)
                {
                    e = 1f;
                    solaates();
                }
                if (scaler.x <= 0 && a <= 0)
                {
                    a = 5f;
                    solaates();
                }
                if (scaler.x <= 0 && b <= 0)
                {
                    solaates();
                    b = 5f;
                }
                if (scaler.x <= 0 && c <= 0)
                {
                    solaates();
                    c = 5f;
                }
                if (scaler.x <= 0 && d <= 0)
                {
                    solaates();
                    bulletattackTimer2 = secondskillcooldawn;
                    e = 0f;
                    a = 0.35f;
                    b = 0.7f;
                    c = 1.05f;
                    d = 1.4f;
                }
            }
            else
            {
                anim.SetTrigger("Enemysecondskill12");
                if (scaler.x > 0 && e <= 0)
                {
                    e = 1f;
                    sagaates();
                }
                if (scaler.x > 0 && a <= 0)
                {
                    a = 5f;
                    sagaates();
                }
                if (scaler.x > 0 && b <= 0)
                {
                    sagaates();
                    b = 5f;
                }
                if (scaler.x > 0 && c <= 0)
                {
                    sagaates();
                    c = 5f;
                }
                if (scaler.x > 0 && d <= 0)
                {
                    sagaates();
                    bulletattackTimer2 = secondskillcooldawn;
                    e = 0f;
                    a = 0.35f;
                    b = 0.7f;
                    c = 1.05f;
                    d = 1.4f;
                }
            }
        }
        else
        {
            bulletattackTimer2 -= Time.deltaTime;
        }
    }
    public void solaates()
    {
        GameObject mermi = Instantiate(bullet2, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = mermi.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.left * bulletSpeed2;
        scaler = rb.transform.localScale;
        scaler.x = -0.2f;
        rb.transform.localScale = scaler;
    }
    public void sagaates()
    {
        GameObject mermi = Instantiate(bullet2, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = mermi.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.right * bulletSpeed2;

        scaler = rb.transform.localScale;
        scaler.x = 0.2f;
        rb.transform.localScale = scaler;
    }
}




