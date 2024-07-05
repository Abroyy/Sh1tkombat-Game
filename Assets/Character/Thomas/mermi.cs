using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class mermi : MonoBehaviour
{

    public ParticleSystem particle;
    public SpriteRenderer sprite;
    public int attackDamage = 7;
    void Start()
    {
        particle.Stop();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        AI1 a = collision.gameObject.GetComponent<AI1>();
        AI2 b = collision.gameObject.GetComponent<AI2>();
        AI3 c = collision.gameObject.GetComponent<AI3>();
        AI4 d = collision.gameObject.GetComponent<AI4>();
        AI5 e = collision.gameObject.GetComponent<AI5>();
        AI6 f = collision.gameObject.GetComponent<AI6>();
        AI7 g = collision.gameObject.GetComponent<AI7>();
        AI8 h = collision.gameObject.GetComponent<AI8>();
        AI9 i = collision.gameObject.GetComponent<AI9>();
        AI10 j = collision.gameObject.GetComponent<AI10>();
        AI11 k = collision.gameObject.GetComponent<AI11>();
        AI12 l = collision.gameObject.GetComponent<AI12>();
        AI13 m = collision.gameObject.GetComponent<AI13>();
        AI14 n = collision.gameObject.GetComponent<AI14>();

        if (a != null)
        {
            a.TakeDamage(attackDamage);
        }
        else if (b != null)
        {
            b.TakeDamage(attackDamage);
        }
        else if (c != null)
        {
            c.TakeDamage(attackDamage);
        }
        else if (d != null)
        {
            d.TakeDamage(attackDamage);
        }
        else if (e != null)
        {
            e.TakeDamage(attackDamage);
        }
        else if (f != null)
        {
            f.TakeDamage(attackDamage);
        }
        else if (g != null)
        {
            g.TakeDamage(attackDamage);
        }
        else if (h != null)
        {
            h.TakeDamage(attackDamage);
        }
        else if (i != null)
        {
            i.TakeDamage(attackDamage);
        }
        else if (j != null)
        {
            j.TakeDamage(attackDamage);
        }
        else if (k != null)
        {
            k.TakeDamage(attackDamage);
        }
        else if (l != null)
        {
            l.TakeDamage(attackDamage);
        }
        else if (m != null)
        {
            m.TakeDamage(attackDamage);
        }
        else if (n != null)
        {
            n.TakeDamage(attackDamage);
        }
        particle.Play();
        sprite.enabled = false;
        
        Destroy(gameObject,0.4f);
    }
}