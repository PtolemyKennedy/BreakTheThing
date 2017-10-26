using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AoECubeBreaker : MonoBehaviour
{

    Collider[] hitColliders;
    public float sploderRadius;
    public float explosionForce;

	// Use this for initialization
	void Start ()
    {
        //add this button to the "explode the thing" buttons on click() so when it is clicked it calls the Explode() function
        UnityEngine.Events.UnityAction action1 = () => { Explode(); };
        GameObject.FindGameObjectWithTag("ExplodeTheThingButton").GetComponent<Button>().onClick.AddListener(action1);
    }
	
	// Update is called once per frame
	void Update ()
    {      
       
    }

    void FindObjectsInRadius(Vector3 center, float radius)
    {
        hitColliders = Physics.OverlapSphere(center, radius);       
        
    }

    public void Explode()
    {
        //find all objects in radius
        FindObjectsInRadius(transform.position, sploderRadius);

        foreach (Collider col in hitColliders)
        {
            //if it can be sploded
            if (col.gameObject.tag == "splodable")
            {
                //add force
                col.gameObject.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, sploderRadius);
            }
        }

        //pretty explosion stuff

        Destroy(gameObject);
    }
}
