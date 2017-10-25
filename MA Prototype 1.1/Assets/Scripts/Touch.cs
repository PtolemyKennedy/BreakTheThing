using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class Touch : MonoBehaviour
{
    public GameObject cameraRotator;
    public Camera mainCamera;

    Vector2 touchPosition;

    float pinchLength = 0;

    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) //this for android
        //{
        //    // Check if finger is over a UI element
        //    if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        //    {
                
        //    }
        //}

        if (!EventSystem.current.IsPointerOverGameObject()) //this for pc
        {            
            if (Input.GetMouseButtonUp(0) || Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                //will need to be changed when more explosives are added in, maybe add explosion radius to the list?

                //find the position that was clicked
                Vector3 pos = Vector3.zero;

                Ray ray = Camera.main.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    pos = hit.point;
                }

                //instantiate a splodie thing where clicked
                GameObject go = Instantiate(Resources.Load("SplodieStuff"), pos, Quaternion.identity) as GameObject;
            }
        }
       
        //rotation and height check
        if (Input.GetTouch(0).phase == TouchPhase.Moved && Input.touchCount == 1)
        {

            //rotate camera
            if (/*xDelta < xDeadzone*/ touchPosition.x < Input.GetTouch(0).position.x /*&& (touchPosition.x + Input.GetTouch(0).position.x) > 1*/)
            {
                //rotate the camera left around the center
                cameraRotator.GetComponent<Transform>().Rotate(new Vector3(0, 2, 0));
            }
            else if (/*yDelta < yDeadzone*/touchPosition.x > Input.GetTouch(0).position.x)
            {
                //rotate the camera right around the center
                cameraRotator.GetComponent<Transform>().Rotate(new Vector3(0, -2, 0));
            }



            if (touchPosition.y < Input.GetTouch(0).position.y)
            {
                //move the camera up to a limit
                if (cameraRotator.GetComponent<Transform>().position.y <= 15)
                {
                    cameraRotator.GetComponent<Transform>().Translate(new Vector3(0, 0.15f, 0));
                }
            }
            else if (touchPosition.y > Input.GetTouch(0).position.y)
            {
                //move the camera down to a limit
                if (cameraRotator.GetComponent<Transform>().position.y >= 2)
                {
                    cameraRotator.GetComponent<Transform>().Translate(new Vector3(0, -0.15f, 0));
                }
            }

            touchPosition = Input.GetTouch(0).position;
        }

        //zoom and pinch check
        if (Input.touchCount == 2)
        {
            //compare lengths and zoom accordingly
            if (pinchLength < Vector3.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position))
            {
                //zoom in
                if (mainCamera.GetComponent<Transform>().localPosition.z < -10)
                {
                    mainCamera.GetComponent<Transform>().Translate(new Vector3(0, 0, 0.5f));
                }
                
            }
            else if (pinchLength > Vector3.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position))
            {
                //zoom out
                if (mainCamera.GetComponent<Transform>().localPosition.z > -20)
                {
                    mainCamera.GetComponent<Transform>().Translate(new Vector3(0, 0, -0.5f));
                }
            }

            //get length between both touches
            pinchLength = Vector3.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
        }
	}
}
