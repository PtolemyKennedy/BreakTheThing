using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoECubeBreaker : MonoBehaviour
{
    Collider[] hitColliders;
    public float sploderRadius = 5;

	// Use this for initialization
	void Start ()
    {
       
	}
	
	// Update is called once per frame
	void Update ()
    {      
       
    }

    void FindCubesInRadius(Vector3 center, float radius)
    {
        hitColliders = Physics.OverlapSphere(center, radius);       
        
    }

    public void Explode()
    {
        //find all objects in radius
        FindCubesInRadius(transform.position, sploderRadius);
        //set is kinematic in the ridgidbody component to false for all in radius
        foreach (Collider col in hitColliders)
        {
            //if it cab be sploded
            if (col.gameObject.tag == "splodable")
            {
                //Destroy(col.gameObject.GetComponent<FixedJoint>());
                //col.gameObject.GetComponent<Rigidbody>().isKinematic = false;

                col.gameObject.GetComponent<Rigidbody>().AddExplosionForce(500, transform.position, sploderRadius);
            }

        }
    }

}
