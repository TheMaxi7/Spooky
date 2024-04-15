using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple;

public class Shooting : MonoBehaviour
{
    public Camera cam;
    private Vector3 destination;
    public Transform firePoint;
    public GameObject projectilePrefab;
    public GameObject chargeProjectilePrefab;
    public float projectileSpeed = 30f;
    private float timeToFire;
    public float fireRate = 4;
    public float arcRange;
    private bool isCharging = false;
    public float chargeSpeed;
    private float chargeTime;
    public int chargeSpellCost = 10;
    public int baseSpellCost = 1;

    public AudioSource basicSpell;
    public AudioSource chargedSpell;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= timeToFire && Summoning.summoning == false && UiManager.playerMana >= baseSpellCost)
        {
            timeToFire = Time.time + 1/fireRate;
            UiManager.playerMana -= baseSpellCost;
            Shoot(projectilePrefab);
            basicSpell.Play();
        }

        if (Input.GetButton("Fire2") && chargeTime<2 && UiManager.playerMana >= chargeSpellCost)
        {
            isCharging=true;
            if(isCharging)
            {
                chargeTime += Time.deltaTime * chargeSpeed;
            }
        }
        if (Input.GetButton("Fire2") && chargeTime >= 2 && UiManager.playerMana >= chargeSpellCost)
        {
            Shoot(chargeProjectilePrefab);
            chargedSpell.Play();
            UiManager.playerMana -= chargeSpellCost;
            isCharging = false;
            chargeTime = 0;
        }
    }


    void Shoot(GameObject spell)
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
           
        }
        else
        {
            destination = ray.GetPoint(1000);
        }

        InstantiateProjectile(firePoint, spell);
    }

    

    void InstantiateProjectile(Transform firePoint, GameObject spell)
    {
        var projectileObj = Instantiate(spell, firePoint.position, Quaternion.identity);
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * projectileSpeed;

        iTween.PunchPosition(projectileObj, new Vector3(Random.Range(-arcRange, arcRange), Random.Range(-arcRange, arcRange), 0), Random.Range(0.5f, 2));
    }
}
