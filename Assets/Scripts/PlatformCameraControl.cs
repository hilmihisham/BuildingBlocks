using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCameraControl : MonoBehaviour
{
    public float speed = 30.0f;

    // target position will be default Vector3
    Vector3 targetPosition = new Vector3(0.5f, 0.5f, 0.5f);

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(targetPosition);
        //transform.localRotation(new Vector3(35f, 0f, 0f));

        #region ZOOM IN/OUT
        if (Input.GetKey(KeyCode.LeftShift))
            if(Input.GetKey(KeyCode.UpArrow))
                transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift))
            if (Input.GetKey(KeyCode.DownArrow))
                transform.Translate(-Vector3.forward * speed * Time.deltaTime);
        #endregion

        #region ROTATE VERTICAL/HORIZONTAL
        if (Input.GetKey(KeyCode.RightArrow))
            transform.RotateAround(targetPosition, -Vector3.up, Time.deltaTime * speed);
        if (Input.GetKey(KeyCode.LeftArrow))
            transform.RotateAround(targetPosition, Vector3.up, Time.deltaTime * speed);


        if (Input.GetKey(KeyCode.UpArrow))
            transform.RotateAround(targetPosition, Vector3.right, Time.deltaTime * speed);
        if (Input.GetKey(KeyCode.DownArrow))
            transform.RotateAround(targetPosition, -Vector3.right, Time.deltaTime * speed);
        #endregion
    }
}
