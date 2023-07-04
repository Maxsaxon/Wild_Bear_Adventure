using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDamage : MonoBehaviour
{
    public LayerMask layer;
    public float radius = 1f;
    public float damage = 20f;

    // Update is called once per frame
    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, layer); // layer means the circle will search for collision with player in that circle

        if (hits.Length > 0) // if we have at least one hit
        {
            //print("Touched the game object");

            hits[0].GetComponent<HealthPoints>().ApplyDamage(damage);

            gameObject.SetActive(false); // deactivate the attack point
        }
    }
}
