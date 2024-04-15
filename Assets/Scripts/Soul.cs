using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Soul : MonoBehaviour
{
    public LayerMask whatIsPlayer;
    public float range = 10f;
    private bool playerIsNear = false;
    private float harvestSpeed = 20f;
    public Rigidbody rb;
    public Transform player;
    void Start()
    {
        player = GameObject.Find("PlayerCapsule").transform;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        playerIsNear = Physics.CheckSphere(transform.position, range, whatIsPlayer);
        if (playerIsNear )
        {
            GetHarvested();
        }
    }

    void GetHarvested()
    {
        rb.velocity = (player.position - transform.position).normalized * harvestSpeed;
        Vector3 distanceFromPlayer = player.position - transform.position;
        if (distanceFromPlayer.magnitude <= 1)
        {
            UiManager.soulsNumber++;
            Destroy(gameObject);
        }
    }
}
