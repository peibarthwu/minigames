using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dotgame : MonoBehaviour
{

    private LineRenderer lr;
    // private dynamic int numDots = 9;
    int numRows = 3;
    int numCols = 3;
    int space = 2;
    private GameObject[] sphereObjects = new GameObject[9];

    void Awake()
    {
        int count = 0;
        for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numCols; j++)
                    {
                        sphereObjects[count] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        sphereObjects[count].name = "Sphere" + "_" + i + "_" + j;
                        sphereObjects[count].transform.position = new Vector3(i * space, j * space, 0);
                        count ++;
                    }
            }    
    }

    void DrawLine(Vector3 start, Vector3 end)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.SetWidth(0.1f, 0.1f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetMouseButtonDown (0)){ 
                RaycastHit hit; 
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
                if ( Physics.Raycast (ray,out hit,100.0f)) {
                    Debug.Log("You selected the " + hit.transform.name); // ensure you picked right object
                }
            }
    }
}


