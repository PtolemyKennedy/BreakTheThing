using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Touch : MonoBehaviour
{
    public GameObject  cameraRotator;
    public Camera mainCamera;

    float doubleTapStartTime = 0;
    float doubleTapLengthTime = 0.5f;

    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    Vector2 touchPosition;
    bool moved = false;
    float xDeadzone = 10;
    float yDeadzone = 10;


    float pinchLength = 0;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonUp(0) || Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            //instantiate a splodie thing where clicked
            Vector3 pos = Vector3.zero;

            Ray ray = Camera.main.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                pos = hit.point;
            }   
            
            Instantiate(Resources.Load("SplodieStuff"), pos, Quaternion.identity);
        }

        //touch check
        //if (Input.GetTouch(0).phase == TouchPhase.Ended)
        //{
        //    //screen has stopped being touched

        //    //double tap check
        //    if (doubleTapStartTime + doubleTapLengthTime >= Time.time)
        //    {
        //        //screen has been double tapped

        //        doubleTapStartTime = 0;

        //        if (!moved)
        //        {
        //            //spawn bigger cube
        //            Instantiate(Resources.Load("BigCube"), Vector3.zero, Quaternion.identity);
        //        }
        //    }
        //    else
        //    {
        //        //record time for double tap check
        //        doubleTapStartTime = Time.time;

        //        if (!moved)
        //        {
        //            //spawn cube
        //            Instantiate(Resources.Load("Cube"), Vector3.zero, Quaternion.identity);
        //        }
        //    }
        //    moved = false;
        //}

        //rotation and height check
        if (Input.GetTouch(0).phase == TouchPhase.Moved && Input.touchCount == 1)
        {
            moved = true;
            //touch swipe camera rotation check
            //float xDelta = touchPosition.x - Input.GetTouch(0).position.x;
            //float yDelta = touchPosition.y - Input.GetTouch(0).position.y;
            //rotation deadzone

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



        //if (Input.touches.Length > 0)
        //{
        //    UnityEngine.Touch t = Input.GetTouch(0);
        //    if (t.phase == TouchPhase.Began)
        //    {
        //        //save began touch 2d point
        //        firstPressPos = new Vector2(t.position.x, t.position.y);
        //    }
        //    if (t.phase == TouchPhase.Ended)
        //    {
        //        //save ended touch 2d point
        //        secondPressPos = new Vector2(t.position.x, t.position.y);

        //        //create vector from the two points
        //        currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

        //        //normalize the 2d vector
        //        currentSwipe.Normalize();

        //        //swipe upwards
        //        if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
        //        {
        //            //move the camera up to a limit
        //            if (cameraRotator.GetComponent<Transform>().position.y <= 15)
        //            {
        //                cameraRotator.GetComponent<Transform>().Translate(new Vector3(0, 0.25f, 0));
        //            }
        //        }
        //        //swipe down
        //        if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
        //        {
        //            //move the camera down to a limit
        //            if (cameraRotator.GetComponent<Transform>().position.y >= 2)
        //            {
        //                cameraRotator.GetComponent<Transform>().Translate(new Vector3(0, -0.25f, 0));
        //            }
        //        }
        //        //swipe left
        //        if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
        //        {
        //            //rotate the camera left around the center
        //            cameraRotator.GetComponent<Transform>().Rotate(new Vector3(0, 3, 0));
        //        }
        //        //swipe right
        //        if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
        //        {
        //            //rotate the camera right around the center
        //            cameraRotator.GetComponent<Transform>().Rotate(new Vector3(0, -3, 0));
        //        }
        //    }
        //}

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
