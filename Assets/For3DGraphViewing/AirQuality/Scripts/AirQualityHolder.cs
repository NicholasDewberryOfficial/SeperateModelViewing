//https://stackoverflow.com/questions/41194515/unity-create-ui-control-from-script
// maybe check this out https://bitbucket.org/UnityUIExtensions/unity-ui-extensions/wiki/UPMInstallation
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Fluent;
using System.Reflection;
using System.Linq;
public class AirQualityHolder : MonoBehaviour
{
    public bool isOpen = false;


    public float Ozone;
    public float SolarR;
    public float Wind;
    public float Temp;
    public float Month;
  
    public float Day;

    public float rand1;
    public float rand2;
    public float rand3;
    public float rand4;
    public float rand5;
    public GameObject nearest;
    //*private Vector3 direction;
    //*is for when I tried to use Linecast to create a line between objects
    //as it currently stands, i get a thin red line rather than a full blanket 

    void awake(){

    }
    // Start is called before the first frame update
    void Start() {        
        GameObject[] gameObjectsToConsider = GameObject.FindGameObjectsWithTag("cube");
       //var nearest = gameObjectsToConsider.OrderBy(t=>(t.position - referencePos).sqrMagnitude).Take(3)   //or use .FirstOrDefault();  if you need just one.ToArray();

         //nearest = gameObjectsToConsider.OrderBy(t=> Vector3.Distance(transform.position, t.transform.position).FirstOrDefault());

         //element atone takes the closest object (because element at 0 takes itself)
      nearest = gameObjectsToConsider.OrderBy(t=> Vector3.Distance(transform.position, t.transform.position)).ElementAt(1);//Take(2);


//NOTE: below parts were when i tried to make a surface mesh between every object in the graph, I moved away from this 
//including it for completion's sake, please dont reuse it as it is not necessary
      //RAY STRAT: OUTDATED
        //  var direction
        // direction = (transform.position - nearest.transform.position ).normalized;
        //  Ray ray = new Ray(transform.position, direction);
        //  ray.startWidth = 2f;
        //  ray.endWidth = 2f;



    // SET LINERENDERER FOR LINEREDNERER STRAT
        // LineRenderer lr = GetComponent<LineRenderer>();
        // lr.SetPosition(0, transform.position);
        // lr.SetPosition(1, nearest.transform.position);

    //     //for loop to create the line renderer runs into an issue because it setposition can only take two points, a start and an end.
    //     // for (int i=0; i < gameObjectsToConsider.Length; i++){
    //     //     lr.SetPosition(i, gameObjectsToConsider[i].transform.position);
    //     // }
    //   //  lr.material.SetColor("Lcolor", new Color(transform.position.x, transform.position.y, transform.position.z, 1.0f));
    //    //lr.material.color = new Color(transform.position.x, transform.position.y, transform.position.z, .8f);//1.0f);
    //     //lr.material.color = new Color.HSVToRGB(transform.position.x, transform.position.y, 1);
    //     lr.material.color = new Color (1, rand2, 1, 1.0f );
        // Color c1 = new Color(transform.position.x, transform.position.y, transform.position.z, 1.0f);
        // Color c2 = new Color(nearest.transform.position.x, nearest.transform.position.y, nearest.transform.position.z, 1.0f);
        // //lr.SetColors(c1, c2);
        // lr.startColor = c1;
        // lr.endColor = c2;
   }
    

    // Update is called once per frame
    void Update()
    {
        //*Debug.DrawRay(ray);

        //if 
    }

    void OnMouseDown(){ 
        ClearLog();
        Debug.Log("The details of your selection are:");
        Debug.Log("Ozone is " + Ozone);
        Debug.Log("Solar.R " + SolarR);
        Debug.Log("Wind is " + Wind);
        Debug.Log("Temp is " + Temp);
        Debug.Log("Month is " + Month);
        Debug.Log("Day is " + Day);
        Debug.Log("rand1 is " + rand1);
        Debug.Log("rand2 is " + rand2);
        Debug.Log("rand3 is " + rand3);
        Debug.Log("rand4 is " + rand4);
        Debug.Log("rand5 is " + rand5);
    }

    public void ClearLog()
{
    var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
    var type = assembly.GetType("UnityEditor.LogEntries");
    var method = type.GetMethod("Clear");
    method.Invoke(new object(), null);
}

    

    }



