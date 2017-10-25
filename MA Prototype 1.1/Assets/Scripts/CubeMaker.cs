using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CubeDimensions
{
    public int x, y, z;
}

public class CubeMaker : MonoBehaviour
{
    public int cubeX;
    public int cubeY;
    public int cubeZ;
    
    public float offset; //distance between cube centers

    public float breakForce;

    public GameObject CubePrefab;

    

    // Use this for initialization
    void Start()
    {
        int cubeXCount = 0;
        int cubeYCount = 0;
        int cubeZCount = 0;
        int count = 0;
        //GameObject[] array = new GameObject[cubeX * cubeY * cubeZ];
        GameObject[,,] array = new GameObject[10, 10, 10];

        for (float i = 0; i < cubeX*offset; i+=offset)
        {
            for (float j = 0; j < cubeY*offset; j+=offset)
            {
                for (float k = 0; k < cubeZ*offset; k+=offset)
                {
                    GameObject go;
                    //for each cube instantiate one and then attach it to the previous using the fixed joint
                    go = Instantiate(CubePrefab, new Vector3(i, j + 5.6f, k), new Quaternion(0, 0, 0, 0));

                    //add to array
                    //array[count] = go;

                    //Debug.Log(cubeXCount+", "+ cubeYCount+", "+ cubeZCount);

                    array[cubeXCount, cubeYCount, cubeZCount] = go;

                    //count++;
                    if (cubeXCount > cubeX)
                    {
                        cubeXCount = 0;
                    }
                    else
                    {
                        cubeXCount++;
                    }
                    
                }
                if (cubeYCount > cubeY)
                {
                    cubeYCount = 0;
                }
                else
                {
                    cubeYCount++;
                }               
            }
            if (cubeZCount > cubeZ)
            {
                cubeZCount = 0;
            }
            else
            {
                cubeZCount++;
            }
        }

        //connect them with fixed joints
        //for (int i = 0; i < cubeX * cubeX; i += cubeX)
        //{
        //    for (int k = 0; k < cubeZ - 1; k++)
        //    {
        //        array[k + i].GetComponent<FixedJoint>().connectedBody = array[k + i + 1].GetComponent<Rigidbody>();              
        //    }
        //    //connect the edge cubes to the inner edge cubes
        //    array[cubeZ + i - 1].GetComponent<FixedJoint>().connectedBody = array[cubeZ + i - 2].GetComponent<Rigidbody>();
        //    //connect them the other way 
        //    array[i].GetComponent<FixedJoint>().connectedBody = array[i+ cubeX].GetComponent<Rigidbody>();
        //    array[i + cubeZ - 1].GetComponent<FixedJoint>().connectedBody = array[i + cubeX + cubeZ - 1].GetComponent<Rigidbody>();
        //}

        FixedJoint fj;

        for (int i = 0; i < cubeX; i++)
        {
            for (int j = 0; j < cubeY; j++)
            {
                for (int k = 0; k < cubeZ; k++)
                {
                    fj = array[i, j, k].AddComponent<FixedJoint>() as FixedJoint;
                    fj.breakForce = breakForce;
                    fj.connectedBody = array[i + 1, j, k].GetComponent<Rigidbody>();

                    //fj = array[i, j, k].AddComponent<FixedJoint>() as FixedJoint;
                    //fj.breakForce = breakForce;
                    //fj.connectedBody = array[i, j + 1, k].GetComponent<Rigidbody>();

                    fj = array[i, j, k].AddComponent<FixedJoint>() as FixedJoint;
                    fj.breakForce = breakForce;
                    fj.connectedBody = array[i, j, k + 1].GetComponent<Rigidbody>();

                    //fj = array[i, j, k].AddComponent<FixedJoint>() as FixedJoint;
                    //fj.breakForce = breakForce;
                    //fj.connectedBody = array[i - 1, j, k].GetComponent<Rigidbody>();

                    //fj = array[i, j, k].AddComponent<FixedJoint>() as FixedJoint;
                    //fj.breakForce = breakForce;
                    //fj.connectedBody = array[i, j - 1, k].GetComponent<Rigidbody>();

                    //fj = array[i, j, k].AddComponent<FixedJoint>() as FixedJoint;
                    //fj.breakForce = breakForce;
                    //fj.connectedBody = array[i, j, k - 1].GetComponent<Rigidbody>();
                }
            }
        }

        //for (int i = 0; i < cubeX - 1; i++)
        //{
        //    array[i].GetComponent<FixedJoint>().connectedBody = array[i + 1].GetComponent<Rigidbody>();
        //}
        //for (int j = 0; j < cubeY - 1; j++)
        //{
        //    array[j].GetComponent<FixedJoint>().connectedBody = array[j + 1].GetComponent<Rigidbody>();
        //}


    }
	// Update is called once per frame
	void Update ()
    {
		
	}
}
