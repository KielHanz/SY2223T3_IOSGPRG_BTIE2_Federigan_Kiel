using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Slash : MonoBehaviour
{


    public float slashDirection;

    Vector3 firstPos;
    Vector3 lastPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;
            if (touch.phase == TouchPhase.Began)
            {
                firstPos = touchPosition;
                //Debug.Log("First position" + firstPos);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                lastPos = touchPosition;


                Vector2 distance = lastPos - firstPos;
                float distanceX = Mathf.Abs(distance.x);
                float distanceY = Mathf.Abs(distance.y);


                if (firstPos.y > lastPos.y && distanceY > distanceX)
                {
                    slashDirection = 1;
                    Debug.Log("Swipe down");
                }
                else if (firstPos.y < lastPos.y && distanceY > distanceX)
                {
                    slashDirection = 2;
                    Debug.Log("Swipe up");
                }
                else if (firstPos.x < lastPos.x && distanceX > distanceY)
                {
                    slashDirection = 3;
                    Debug.Log("Swipe right");
                }
                else if (firstPos.x > lastPos.x && distanceX > distanceY)
                {
                    slashDirection = 4;
                    Debug.Log("Swipe left");
                }

            }

        }
    }
}
