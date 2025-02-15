using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SliceController : MonoBehaviour
{
    //public enum PointerState
    //{
    //    None,
    //    up,
    //    down,
    //    left,
    //    right,
    //    upRight,
    //    downRight,
    //    downLeft,
    //    upLeft,

    //}
    // public PointerState pointerState;
    // Start is called before the first frame update
    [SerializeField] GameObject Pointer;
    [SerializeField] GameObject Player;
    Vector3 initPos = Vector3.zero;
    Vector3 finalPos = Vector3.zero;
    [SerializeField] float sensitivity;
    float angle;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 p = Input.mousePosition;
            Vector3 pos = Camera.main.ScreenToWorldPoint(p);
            pos.z = 0;

            initPos = pos;
            p.z = 0;
            //transform.position = pos;   
            //Debug.Log("button pressed at "+ initPos);

        }
         
        if (Input.GetMouseButtonUp(0))
        {
            // we get the position of the mouse
            Vector3 p = Input.mousePosition;
            Vector3 pos = Camera.main.ScreenToWorldPoint(p);

            // set the fixed / cast position to the object
            finalPos = pos;
            transform.position = finalPos;


            //we calculate the distance between the click and the release.
            Vector3 deltaPos = finalPos - initPos;

            // we calculate the angle from the origin (vector right).
            angle = Vector3.Angle(deltaPos, Vector3.right);
            


            Debug.Log("deltaPos is " + deltaPos);
            if (deltaPos.y < 0)
            {
                // this case says that the pointer goes down
                angle *= -1;

            }

            print("angle is " + angle);
            angleFormatter(angle);
            
            
            
            ///transform.position = Vector3.zero;

        }
    }
    void angleFormatter(float angle)
    {
        //string[]= { }

        sensitivity = 45;
        //
        if (angle >= -sensitivity / 2 && angle <= sensitivity / 2)
        {
            //señal right;
            Player.GetComponent<PlayerMovement>().ChangeSpeed(new string[] { "right" });
            print("right");
        }
        else if (angle > sensitivity / 2 && angle <= 90 - sensitivity / 2)
        {
            //señal right
            //señal up
            Player.GetComponent<PlayerMovement>().ChangeSpeed(new string[] { "right","up" });
            print("right + up");

        }
        else if (angle > 90 - sensitivity / 2 && angle <= 90 + sensitivity / 2)
        {
            //señal up;
            Player.GetComponent<PlayerMovement>().ChangeSpeed(new string[] { "up" });
            print("up");
        }
        else if (angle > 90 + sensitivity / 2 && angle <= 180 - sensitivity / 2)
        {
            //señal up
            //señal left
            Player.GetComponent<PlayerMovement>().ChangeSpeed(new string[] { "left","up" });
            print("left + up");

        }
        else if (angle > 180 - sensitivity / 2 || angle < -180 + sensitivity / 2)
        {
            //señal left;
            Player.GetComponent<PlayerMovement>().ChangeSpeed(new string[] { "left" });
            print("left");
        }
        else if (angle < -sensitivity / 2 && angle >= -90 + sensitivity / 2)
        {
            //señal up
            //señal left
            //print("up + left");

            print("right + down");
        }
        else if (angle < -90 + sensitivity / 2 && angle >= -90 - sensitivity / 2)
        {
            //señal left
            //print("left");
            Player.GetComponent<PlayerMovement>().ChangeSpeed(new string[] { "down" });
            print("down");
        }
        else if (angle < -90 - sensitivity / 2 && angle >= -180 + sensitivity / 2)
        {
            //señal left
            //señal down
            print("left+down");
        }
    }



}

