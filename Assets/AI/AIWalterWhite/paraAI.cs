using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paraAI : MonoBehaviour
{
    public ParticleSystem particle;
    public SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
      particle.Stop();
    }
    public int attackDamage = 15;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Char1 char1 = collision.gameObject.GetComponent<Char1>();
        Char2 char2 = collision.gameObject.GetComponent<Char2>();
        Char3 char3 = collision.gameObject.GetComponent<Char3>();
        Char4 char4 = collision.gameObject.GetComponent<Char4>();
        Char5 char5 = collision.gameObject.GetComponent<Char5>();
        Char6 char6 = collision.gameObject.GetComponent<Char6>();
        Char7 char7 = collision.gameObject.GetComponent<Char7>();
        Char8 char8 = collision.gameObject.GetComponent<Char8>();
        Char9 char9 = collision.gameObject.GetComponent<Char9>();
        Char10 char10 = collision.gameObject.GetComponent<Char10>();
        Char11 char11 = collision.gameObject.GetComponent<Char11>();
        Char12 char12 = collision.gameObject.GetComponent<Char12>();
        Char13 char13 = collision.gameObject.GetComponent<Char13>();
        Char14 char14 = collision.gameObject.GetComponent<Char14>();

        if (char1 != null)
        {
            char1.CharacterTakeDamage(attackDamage);
        }
        else if (char2 != null)
        {
            char2.CharacterTakeDamage(attackDamage);
        }
        else if (char3 != null)
        {
            char3.CharacterTakeDamage(attackDamage);
        }
        else if (char4 != null)
        {
            char4.CharacterTakeDamage(attackDamage);
        }
        else if (char5 != null)
        {
            char5.CharacterTakeDamage(attackDamage);
        }
        else if (char6 != null)
        {
            char6.CharacterTakeDamage(attackDamage);
        }
        else if (char7 != null)
        {
            char7.CharacterTakeDamage(attackDamage);
        }
        else if (char8 != null)
        {
            char8.CharacterTakeDamage(attackDamage);
        }
        else if (char9 != null)
        {
            char9.CharacterTakeDamage(attackDamage);
        }
        else if (char10 != null)
        {
            char10.CharacterTakeDamage(attackDamage);
        }
        else if (char11 != null)
        {
            char11.CharacterTakeDamage(attackDamage);
        }
        else if (char12 != null)
        {
            char12.CharacterTakeDamage(attackDamage);
        }
        else if (char13 != null)
        {
            char13.CharacterTakeDamage(attackDamage);
        }
        else if (char14 != null)
        {
            char14.CharacterTakeDamage(attackDamage);
        }
        particle.Play();
        sprite.enabled = false;


        Destroy(gameObject,0.4f);
    }
}
