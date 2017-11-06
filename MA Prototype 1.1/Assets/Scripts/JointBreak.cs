using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointBreak : MonoBehaviour
{
    PointsSystem ps;
    FixedJoint fj;

	// Use this for initialization
	void Start ()
    {
        ps = GameObject.FindGameObjectWithTag("GameController").GetComponent<PointsSystem>();
        fj = GetComponent<FixedJoint>();      
	}
	
	// Update is called once per frame
	//void Update ()
 //   {
		
	//}

    //when the joint breaks score points relative to how difficult the joint was to break
    private void OnJointBreak(float breakForce)
    {
        ps.ScorePoints(fj.breakForce);
    }
}
