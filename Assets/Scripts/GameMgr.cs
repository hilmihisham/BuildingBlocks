using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour {

    public GameObject cubePrefab; // building cube
    public GameObject cubeTransparent; // guiding cube

    // initializing translucent color
    private Color32 yellow = new Color32(255, 135, 0, 150);
    private Color32 green = new Color32(0, 150, 0, 150);

    // position far enough from the camera
    private Vector3 outOfSight = new Vector3(100f, 100f, 100f);

	// Use this for initialization
	void Start () {
        // instantiate guiding cube
        cubeTransparent = Instantiate(cubeTransparent);
	}
	
	// Update is called once per frame
	void Update () {

        // transparent cube will follow cursor (mousePosition)
        RaycastHit hitInfo = new RaycastHit();
        bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);

        if (hit)
        {
            if (hitInfo.transform.tag.Equals("Base")) // if we pointed on base
            {
                // rounding x and z so that the cube will follow exactly box pattern on the base
                // set y to 0.5f to have the cube sit on top of the base
                cubeTransparent.transform.position = new Vector3(
                    Mathf.Round(hitInfo.point.x),
                    0.5f,
                    Mathf.Round(hitInfo.point.z)
                    );

                // set color to yellow
                cubeTransparent.transform.gameObject.GetComponent<Renderer>().material.color = yellow;
            }
            if (hitInfo.transform.tag.Equals("cubeObject")) // if we pointed on another cube
            {
                // get the position of the pointed cube, and + normal so that we will build on top of that cube face
                cubeTransparent.transform.position = new Vector3(
                    hitInfo.transform.position.x + hitInfo.normal.x,
                    hitInfo.transform.position.y + hitInfo.normal.y,
                    hitInfo.transform.position.z + hitInfo.normal.z
                    );

                // set color to green
                cubeTransparent.transform.gameObject.GetComponent<Renderer>().material.color = green;
            }
        }
        else
        {
            // if no hit, remove the transparent cube away from the view
            cubeTransparent.transform.position = outOfSight;
        }

        if (Input.GetMouseButtonUp(0) && (cubeTransparent.transform.position != outOfSight))
        {
            // instantiating cube by the position of transparent cube
            // (building on top of other cube have been calculated from positioning transparent cube, so no worries)
            GameObject cube = Instantiate(
                cubePrefab,
                cubeTransparent.transform.position,
                Quaternion.identity
                );
            cube.tag = "cubeObject"; // assign tag to cube
        }

    }

}
