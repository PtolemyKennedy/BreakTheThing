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

    /// <summary>
    /// is the player in first person mode
    /// </summary>
    bool isFirstPerson = false;

    /// <summary>
    /// the rotation speed of the camera
    /// </summary>
    float rotateSpeed = 1.8f;

    /// <summary>
    /// the height change speed of the camera
    /// </summary>
    float heightSpeed = 0.12f;

    /// <summary>
    /// is the player currently zooming in/out
    /// </summary>
    bool isZooming = false;

    /// <summary>
    /// initial pinch length for zooming
    /// </summary>
    float startPinchLength = 0;

    /// <summary>
    /// change in pinch length for zooming
    /// </summary>
    float deltaPinch = 0;

    /// <summary>
    /// the size of the pinch deadzone for zooming
    /// </summary>
    public float pinchDeadzone = 5;

    /// <summary>
    /// minimum zoom distance, uses local position of camera
    /// </summary>
    float minZoom = 0;

    /// <summary>
    /// maximum zoom distance, uses local position of camera
    /// </summary>
    float maxZoom = 50;

    /// <summary>
    /// number of explosives used in the current level
    /// </summary>
    int explosivesUsed = 0;

    /// <summary>
    /// maximum number of explosives available for full score on the current level
    /// </summary>
    public int maxExplosives = 3;



    private float vertical = 0;
    private float horizontal = 0;

    // Use this for initialization
    //void Start()
    //{

    //}

    // Update is called once per frame
    void Update ()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonUp(0) /*Input.touchCount = 1 && Input.GetTouch(0).phase == TouchPhase.Ended*/)
            {
                //find the position that was clicked
                Vector3 pos = Vector3.zero;

                Ray ray = Camera.main.GetComponent<Camera>().ScreenPointToRay(/*Input.GetTouch(0).position*/Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    pos = hit.point;
                }

                //instantiate a splodie thing where clicked
                Instantiate(Resources.Load("SplodieStuff"), pos, Quaternion.identity);
                //add 1 to number of bombs used
                explosivesUsed++;
            }

            if (explosivesUsed >= maxExplosives)
            {
                GetComponent<PointsSystem>().IsOverExplosivesCap = true;
            }
        }

        if (!isFirstPerson)
        {
            //rotation and height check
            if (Input.GetTouch(0).phase == TouchPhase.Moved && Input.touchCount == 2)
            {
                //rotate camera
                if (touchPosition.x < Input.GetTouch(0).position.x)
                {
                    //rotate the camera left around the center
                    cameraRotator.GetComponent<Transform>().Rotate(new Vector3(0, rotateSpeed, 0));
                }
                else if (touchPosition.x > Input.GetTouch(0).position.x)
                {
                    //rotate the camera right around the center
                    cameraRotator.GetComponent<Transform>().Rotate(new Vector3(0, -rotateSpeed, 0));
                }
                //move camera up and down
                if (touchPosition.y < Input.GetTouch(0).position.y)
                {
                    //move the camera up to a limit
                    if (cameraRotator.GetComponent<Transform>().position.y <= 15)
                    {
                        cameraRotator.GetComponent<Transform>().Translate(new Vector3(0, heightSpeed, 0));
                    }
                }
                else if (touchPosition.y > Input.GetTouch(0).position.y)
                {
                    //move the camera down to a limit
                    if (cameraRotator.GetComponent<Transform>().position.y >= 2)
                    {
                        cameraRotator.GetComponent<Transform>().Translate(new Vector3(0, -heightSpeed, 0));
                    }
                }

                touchPosition = Input.GetTouch(0).position;
            }


            //zoom and pinch
            if (Input.touchCount == 2)
            {
                //check if currently zooming in/out
                if (!isZooming)
                {
                    //if not record the starting pinch length
                    startPinchLength = Vector3.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
                }
                else
                {
                    float pinchLength;
                    pinchLength = Vector3.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);

                    //determine the change in pinch length from the starting pinch length
                    deltaPinch = pinchLength - startPinchLength;

                    //if the change is greater than the deadzone attempt to zoom in/out
                    if (deltaPinch < -pinchDeadzone)
                    {
                        //if camera is within the max/min zoom then zoom
                        if (mainCamera.GetComponent<Transform>().localPosition.z < maxZoom)
                        {
                            mainCamera.GetComponent<Transform>().Translate(new Vector3(0, 0, -0.5f));
                        }
                    }
                    else if (deltaPinch > pinchDeadzone)
                    {
                        if (mainCamera.GetComponent<Transform>().localPosition.z > minZoom)
                        {
                            mainCamera.GetComponent<Transform>().Translate(new Vector3(0, 0, 0.5f));
                        }
                    }
                }
                ////compare lengths and zoom accordingly
                //if (pinchLength < Vector3.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position))
                //{
                //    //zoom in
                //    if (mainCamera.GetComponent<Transform>().localPosition.z > minZoom)
                //    {
                //        mainCamera.GetComponent<Transform>().Translate(new Vector3(0, 0, 0.5f));
                //    }              
                //}
                //else if (pinchLength > Vector3.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position))
                //{
                //    //zoom out
                //    if (mainCamera.GetComponent<Transform>().localPosition.z < maxZoom)
                //    {
                //        mainCamera.GetComponent<Transform>().Translate(new Vector3(0, 0, -0.5f));
                //    }
                //}

                ////get length between both touches
                //pinchLength = Vector3.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
            }
            //no fingers touching so we stop zooming
            else if (Input.touchCount == 0)
            {
                isZooming = false;
            }
        }
        else //use first person controls
        {
            //code taken from archery game, should work

            horizontal += Input.gyro.rotationRateUnbiased.x;
            //horizontal = Mathf.Clamp(horizontal, -horizontalRange, horizontalRange);

            vertical += Input.gyro.rotationRateUnbiased.y;
            //vertical = Mathf.Clamp(vertical, -verticalRange, verticalRange);

            Camera.main.transform.localRotation = Quaternion.Euler(-horizontal, -vertical, 0);
            Camera.main.transform.Rotate(-Input.gyro.rotationRateUnbiased.x, -Input.gyro.rotationRateUnbiased.y, 0);
        }
	}

    /// <summary>
    /// change the view from first to third person and vice versa
    /// </summary>
    public void ChangeView()
    {
        isFirstPerson = !isFirstPerson;
    }
}
