using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dotgame : MonoBehaviour
{
    public Material orbMat;
    public Material chosenMat; //color of selected orb
    public int difficulty;

    private LineRenderer lr;
    private GameObject[] sphereObjects = new GameObject[9];
    private Transform[] points;

    private Vector3 firstPoint;
    private Vector3 secondPoint;
    private int check = 0;

    int numRows = 3;
    int numCols = 3;
    int space = 2;

    


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
                        sphereObjects[count].GetComponent<Renderer>().material = orbMat;
                        count ++;
                    }
            }    
    }

    void setStart(Vector3 p)
    {

    }

    void setEnd(Vector3 p)
    {
        
    }

    void DrawLine(Vector3 start, Vector3 end)
    {
        // Debug.DrawLine (start, end, Color.red);
        // GameObject myLine = new GameObject();
        // myLine.transform.position = start;
        // myLine.AddComponent<LineRenderer>();
        // LineRenderer lr = myLine.GetComponent<LineRenderer>();
        // lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        // lr.SetWidth(0.1f, 0.1f);
        // lr.SetPosition(0, start);
        // lr.SetPosition(1, end);
        Debug.Log("hit");
    }

    // // Change Color of the orbs
    IEnumerator ChangeColor()
    {   
        for (int i = 0; i < difficulty; i++)
        {
            var chosen = sphereObjects[Random.Range(0,sphereObjects.Length - 1)];
            //Get the Renderer component for sphere
            var renderer = chosen.GetComponent<Renderer>();
            renderer.material = chosenMat;
        
            yield return new WaitForSeconds(0.7f);

            renderer.material= orbMat;
        }

            
       
        
     }

    // Start is called before the first frame update
    void Start()
    {   
        StartCoroutine (ChangeColor());
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetMouseButtonDown (0))
        { 
                RaycastHit hit; 
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
                if ( Physics.Raycast (ray,out hit,100.0f)) 
                {
                    Debug.Log("You selected the " + hit.transform.name); // ensure you picked right object
                    if(check == 0){
                        firstPoint = hit.point;
                        check = 1;
                    }else{
                        secondPoint = hit.point;
                        DrawLine(firstPoint, secondPoint);
                        check = 0;
                    }
                }
         }
    }
}


