using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private bool collided;
    public GameObject impactEffectPrefab;
    public int spellDamage = 30;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(spellDamage);
        }
        else if (collision.gameObject.tag == "Boss")
        {
            collision.gameObject.GetComponent<Boss>().TakeDamage(spellDamage / 10);
        }
        else if (collision.gameObject.tag == "Player")
        {
            UiManager.playerHealth -= spellDamage / 5;
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Projectile"))
        {
            // Ignore collision with other projectiles
            return;
        }

        collided = true;
        var impact = Instantiate(impactEffectPrefab, transform.position, Quaternion.identity);
        Destroy(impact, 2f);
        Destroy(gameObject);

    }
}
