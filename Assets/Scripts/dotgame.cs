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
    
    private List<Vector3> userPoints = new List<Vector3>();
    private List<Vector3> points = new List<Vector3>();

    // private Vector3[] points;
    // private Vector3[] userPoints;

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

    void DrawLine(Vector3 start, Vector3 end)
    {
        lr = (new GameObject("line")).AddComponent<LineRenderer>();
        
        // lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.SetWidth(0.5f, 0.5f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);

        userPoints.Add(firstPoint);

        firstPoint = secondPoint;

        if(userPoints.Count == (points.Count - 1)){
            
            CheckWin();
        }
    }

    // // Change Color of the orbs
    IEnumerator ChangeColor()
    {   
        for (int i = 0; i < difficulty; i++)
        {
            var chosen = sphereObjects[Random.Range(0,sphereObjects.Length - 1)];
            points.Add(chosen.transform.position);

            //Get the Renderer component for sphere
            var renderer = chosen.GetComponent<Renderer>();
            renderer.material = chosenMat;
        
            yield return new WaitForSeconds(1f);

            renderer.material= orbMat;
        }
    }

    void CheckWin()
    {   
        //add final user selected point
        userPoints.Add(firstPoint);
        if (userPoints.Count != points.Count)
        {
            FindObjectOfType<gamemanager>().Lose();
            Debug.Log("Loss");
        }
        else
        {
            for (int i = 0; i < points.Count; i++)
            {
                if (userPoints[i] != points[i])
                {
                    Debug.Log("Loss");
                    FindObjectOfType<gamemanager>().Lose();
                    return;
                }   
            }
            Debug.Log("Win");
            FindObjectOfType<gamemanager>().Win();
            return;
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
                    // Debug.Log("You selected the " + hit.transform.name); // ensure you picked right object
                    if(check == 0){
                        firstPoint = hit.transform.position;
                        check = 1;
                    }else{
                        secondPoint = hit.transform.position;
                        DrawLine(firstPoint, secondPoint);
                    }
                }
         }
    }
}


