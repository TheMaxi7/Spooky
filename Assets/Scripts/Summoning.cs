using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Summoning : MonoBehaviour
{
    public GameObject explosionSpellPrefab;
    public static bool summoning = false;
    public Canvas indicatorCanvas;
    public Image indicatorImage;
    public Camera cam;
    private Ray ray;
    private Vector3 targetPoint;
    public int explosionDamage;
    public float explosionRange;
    public int spellCost;
    public AudioSource explosionSound;

    private void Start()
    {
        indicatorCanvas.enabled = false;
        indicatorImage.enabled = false;
    }
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Input.GetKey(KeyCode.E))
        {
            summoning = true;
        }
        if (summoning)
        {
            ActivateUI();
        }
    }

    private void ActivateUI()
    {
        indicatorCanvas.enabled = true;
        indicatorImage.enabled = true;
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {

            indicatorCanvas.transform.position = hit.point;
            if (Input.GetButtonDown("Fire1") && UiManager.playerMana >= spellCost) 
            {
                targetPoint = hit.point;
                Summon(explosionSpellPrefab, targetPoint);
                UiManager.playerMana-=spellCost;
            }
            if (Input.GetButton("Fire2") && indicatorCanvas.enabled == true && summoning == true)
            {
                indicatorCanvas.enabled = false;
                summoning = false;
            }

        }
    }

    private void Summon(GameObject explosionPrefab, Vector3 explosionPoint)
    {
        
        var explosion = Instantiate(explosionPrefab, explosionPoint, Quaternion.identity);
        explosionSound.Play();
        Destroy(explosion, 1f);

        Collider[] colliders = Physics.OverlapSphere(explosionPoint, explosionRange);

       
        foreach (Collider collider in colliders)
        {
            
            if (collider.CompareTag("Enemy"))
            {
              
                collider.GetComponent<Enemy>().TakeDamage(explosionDamage);

            }
        }
    }
}
