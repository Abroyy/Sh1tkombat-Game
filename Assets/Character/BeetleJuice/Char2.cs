using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;


public class Char2 : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackRange = 0.7f;
    private float attackDamage;
    public float BaseMoveSpeed = 3f;
    public float JumpSpeed = 7.5f;
    private Animator anim;
    private Rigidbody2D rb2d;
    float moveHorizontal;
    public float maxHealth = 300;
    public float meleeAttackTimer = 0f;
    float currentHealth;
    public GameObject bullet;
    public Transform firePoint;
    public float bulletSpeed = 6f;
    public float bulletfiretime = 4f;
    public float bulletattackTimer = 3f;
    public Image HealthBar;
    private float attackTimer = 0f;
    private float a = 0f;
    public float secondskillspeed = 6f;
    public float secondskillAttackDamage = 10f;
    public float secondskillcooldawn = 7f;
    float distance;
    private Transform target;
    private float speedBoostTimer = 0f;
    private bool meleeattackbutton = false;
    private bool firstskillbutton = false;
    private bool secondskillbutton = false;
    private bool isSpeedBoosted = false;
    private bool isDefenceActive = false;
    private bool hasCollided = false;
    private float z = 0f;
    public float characterscalex = 0.18f;
    public bool isGrounded = false;
    private float horizontal =0f;
    private bool ileriHareket = false;
    private bool geriHareket = false;
    Vector2 scaler;
    Vector2 newnewX;

    public GameObject youlose;
    public Button button1;
    public Button button2;
    public AudioSource muzikbutton1;
    public AudioSource muzikbutton2;
    ReklamManager reklam;
    
    

    void Start()
    {
       

        muzikbutton1.Stop();
        muzikbutton2.Stop();
        currentHealth = maxHealth;
        reklam = GetComponent<ReklamManager>();
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        youlose.SetActive(false);
        Time.timeScale = 1;
    }

    void Update()
    {
        horizontal = 0f;

        hareket();
        meleeAttackTimer -= Time.deltaTime;
        Vector2 scaler = transform.localScale;
        CharacterAttack();
        firstSkillShoot();
        CharacterFlip();
        target = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();
        distance = target.position.x - rb2d.position.x;
        secondSkillShoot();
        getSlowed();
    }

    void hareket(){
        if (ileriHareket)
        {
            horizontal = 1f;
        }
        else if (geriHareket)
        {
            horizontal = -1f;
        }

        Vector2 hareket = new Vector2(horizontal * BaseMoveSpeed, rb2d.velocity.y);
        rb2d.velocity = hareket;

    }
    public void OnPressIleri()
    {
        ileriHareket = true;
        anim.SetBool("charwalk2",true);
    }

    public void OnReleaseIleri()
    {
        ileriHareket = false;
        anim.SetBool("charwalk2",false);
    }

    public void OnPressGeri()
    {
        geriHareket = true;
        anim.SetBool("charwalk2",true);

    }

    public void OnReleaseGeri()
    {
        geriHareket = false;
        anim.SetBool("charwalk2",false);

    }


    public void Zipla()
    {
        if(!isGrounded)
        {
          rb2d.velocity = Vector2.up * JumpSpeed;

          isGrounded = true;
        }
        
    }
    
    
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            
            isGrounded = false;
        }
    }
   
    public void enablefirstskill()
    {
        firstskillbutton = true;
    }
    private void disablefirstskill()
    {
        firstskillbutton = false;
    }

    public void enablesecondskill()
    {
       secondskillbutton = true;
    }
    private void disablesecondskill()
    {
        secondskillbutton = false;
    }
    public void enablemeleeskill()
    {
        meleeattackbutton = true;
    }
    private void disablemeleeskill()
    {
        meleeattackbutton = false;
    }
    public void DisableDefence()
    {
        isDefenceActive = false;
        anim.SetBool("chardefence2", false);
    }
    public void EnableDefence()
    {
        isDefenceActive = true;
        anim.SetBool("chardefence2", true);
    }
    public void hizskill()
    {
        if (!isSpeedBoosted)
        {
            isSpeedBoosted = true;
            speedBoostTimer = 0f;
            BaseMoveSpeed = 4f;
            attackTimer = 5f;
            a += 4f;
            meleeAttackTimer += 4f;
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
            a += 4f;
            meleeAttackTimer += 4f;
        }
    }
    private void ResetSpeed()
    {
        isSpeedBoosted = false;
        BaseMoveSpeed = 3f;
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
    void CharacterFlip()
    {
        if (distance < 0)
        {
            scaler = transform.localScale;
            scaler.x = -characterscalex;
            transform.localScale = scaler;
        }
        else
        {
            scaler = transform.localScale;
            scaler.x = characterscalex;
            transform.localScale = scaler;

        }
    }

    void CharacterAttack()
    {
        if (meleeattackbutton==true && meleeAttackTimer <= 0)
        {
            attackDamage = 7;
            DamageEnemy();
            meleeAttackTimer = 2;
            anim.SetTrigger("charmelee2");
            disablemeleeskill();
        }
        else{
            meleeAttackTimer-=Time.deltaTime;
        }
    }
     public void DamageEnemy()
    {

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log(enemy.name);
            if (enemy.name == "ai1(Clone)")
            {
                enemy.GetComponent<AI1>().TakeDamage(attackDamage);
            }
            else if (enemy.name == "ai2(Clone)")
            {
                enemy.GetComponent<AI2>().TakeDamage(attackDamage);
            }
            else if (enemy.name == "ai3(Clone)")
            {
                enemy.GetComponent<AI3>().TakeDamage(attackDamage);
            }
            else if (enemy.name == "ai4(Clone)")
            {
                enemy.GetComponent<AI4>().TakeDamage(attackDamage);
            }
            else if (enemy.name == "ai5(Clone)")
            {
                enemy.GetComponent<AI5>().TakeDamage(attackDamage);
            }
            else if (enemy.name == "ai6(Clone)")
            {
                enemy.GetComponent<AI6>().TakeDamage(attackDamage);
            }
            else if (enemy.name == "ai7(Clone)")
            {
                enemy.GetComponent<AI7>().TakeDamage(attackDamage);
            }
            else if (enemy.name == "ai8(Clone)")
            {
                enemy.GetComponent<AI8>().TakeDamage(attackDamage);
            }
            else if (enemy.name == "ai9(Clone)")
            {
                enemy.GetComponent<AI9>().TakeDamage(attackDamage);
            }
            else if (enemy.name == "ai10(Clone)")
            {
                enemy.GetComponent<AI10>().TakeDamage(attackDamage);
            }
            else if (enemy.name == "ai11(Clone)")
            {
                enemy.GetComponent<AI11>().TakeDamage(attackDamage);
            }
            else if (enemy.name == "ai12(Clone)")
            {
                enemy.GetComponent<AI12>().TakeDamage(attackDamage);
            }
            else if (enemy.name == "ai13(Clone)")
            {
                enemy.GetComponent<AI13>().TakeDamage(attackDamage);
            }
            else if (enemy.name == "ai14(Clone)")
            {
                enemy.GetComponent<AI14>().TakeDamage(attackDamage);
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

    public void CharacterTakeDamage(float damage)
    {
        if (isDefenceActive==true)
        {
            Debug.Log("Zarar engellendi");
            damage -= 4;
            currentHealth -= damage;
        }
        else
        {
            currentHealth -= damage;
        }


        if (currentHealth <= 0)
        {
            //Die();
            Time.timeScale = 0;
            youlose.SetActive(true);

            int currentGold = PlayerPrefs.GetInt("GoldAmount");
            currentGold += 100;
            PlayerPrefs.SetInt("GoldAmount", currentGold);

        }
        HealthBar.fillAmount = currentHealth / 500; 

        void Die()
        {
            anim.SetBool("chardeath2", true);
            this.enabled = false;
        }


    }

    public void Reklamİzle()
    {
        reklam.ShowRewardedAd();
        currentHealth += 150;
        youlose.SetActive(false);
        Time.timeScale = 1;
        reklam.LoadRewardedAd();
    }

    void firstSkillShoot()
    {
        if (firstskillbutton == true && attackTimer <= 0)
        {
            GameObject mermi = Instantiate(bullet, firePoint.position, firePoint.rotation);
            muzikbutton1.Play();

            anim.SetTrigger("charfirstskill2");
            if (scaler.x <=0)
            {
                Rigidbody2D rb = mermi.GetComponent<Rigidbody2D>();
                Vector2 launchDirection = new Vector2(-0.6f, 0.2f);
                rb.velocity = launchDirection * bulletSpeed;                
                attackTimer = bulletfiretime;
                button1.interactable = false;

                scaler = rb.transform.localScale;
                scaler.x = -0.2f;
                rb.transform.localScale = scaler;
                disablefirstskill();

            }
            else
            {
                Rigidbody2D rb = mermi.GetComponent<Rigidbody2D>();
                Vector2 launchDirection = new Vector2(0.6f, 0.2f);
                rb.velocity = launchDirection * bulletSpeed;                
                attackTimer = bulletfiretime;
                button1.interactable = false;

                scaler = rb.transform.localScale;
                scaler.x = 0.2f;
                rb.transform.localScale = scaler;
                disablefirstskill() ;

            }
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }
        if(attackTimer<=0){
            button1.interactable=true;
        }
    }
    void secondSkillShoot()
    {
        if(secondskillbutton == true && a <= 0)
        {
            muzikbutton2.Play();
            button2.interactable = false;
            anim.SetBool("charsecondskill2",true);

            if (z<=0)
            {            
                
                hasCollided=false;
                newnewX = new Vector2(transform.position.x, transform.position.y);
                z = 1f;
            }
            
            float newX = Mathf.MoveTowards(transform.position.x, target.position.x, secondskillspeed*Time.deltaTime);
            transform.position = new Vector2(newX, target.position.y);
            if (hasCollided == true)
            {
                
                anim.SetBool("charsecondskill2",false);
                a = secondskillcooldawn;
                

                attackDamage = secondskillAttackDamage;
                DamageEnemy();
                
                transform.position = new Vector2(newnewX.x,newnewX.y);
                disablesecondskill();
                z = 0f;
            }
        }
        else
        {
            a -= Time.deltaTime;
        }

        if(a<=0){
            button2.interactable=true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            hasCollided = true;

        }
    }
}
