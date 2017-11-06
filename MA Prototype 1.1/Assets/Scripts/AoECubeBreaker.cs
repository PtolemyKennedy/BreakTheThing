using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AoECubeBreaker : MonoBehaviour
{
    Collider[] hitColliders;

    /// <summary>
    /// the radius of effect of the explosive
    /// </summary>
    public float sploderRadius;

    /// <summary>
    /// the force the explosive applies to objects within its radius of effect
    /// </summary>
    public float explosionForce;

	// Use this for initialization
	void Start ()
    {
        //add this button to the "explode the thing" buttons on click() so when it is clicked it calls the Explode() function
        UnityEngine.Events.UnityAction action1 = () => { Explode(); };
        GameObject.FindGameObjectWithTag("ExplodeTheThingButton").GetComponent<Button>().onClick.AddListener(action1);
    }
	
	// Update is called once per frame
	//void Update ()
 //   {      
       
 //   }

        /// <summary>
        /// finds all objects within a given radius of a point and adds them to the hitColliders array
        /// </summary>
        /// <param name="point">the central point of the sphere</param>
        /// <param name="radius">the radius of the sphere</param>
    void FindObjectsInRadius(Vector3 point, float radius)
    {
        hitColliders = Physics.OverlapSphere(point, radius);              
    }


    /// <summary>
    /// explodes the explosive applying force to all objects that can be exploded within its radius of effect
    /// </summary>
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
