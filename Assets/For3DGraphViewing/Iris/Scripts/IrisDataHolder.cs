//https://stackoverflow.com/questions/41194515/unity-create-ui-control-from-script
// maybe check this out https://bitbucket.org/UnityUIExtensions/unity-ui-extensions/wiki/UPMInstallation
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Fluent;
using System.Reflection;
using System.Linq; 
public class IrisDataHolder : MonoBehaviour
{
    public bool isOpen = false;


    public float ID;
    public float sepalLength;
    public float sepalWidth;
    public float petalWidth;
    public float petalLength;
  
    public string species;

    void awake(){

    }
    // Start is called before the first frame update
    void Start() {

    }
    

    // Update is called once per frame
    void Update()
    {
       // var nearest = gameObjectsToConsider.OrderBy(t=> Vector3.Distance(transform.position, t.transform.position).FirstOrDefault());
    }

    void OnMouseDown(){ 
        ClearLog();
        Debug.Log("The details of your selection are:");
        Debug.Log("Petal Length is " + petalLength);
        Debug.Log("Petal Width is " + petalWidth);
        Debug.Log("Sepal Length is " + sepalLength);
        Debug.Log("Petal Width is " + sepalWidth);
        Debug.Log("ID is " + ID);
        Debug.Log("Species is " + species);

    }

    public void ClearLog()
{
    var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
    var type = assembly.GetType("UnityEditor.LogEntries");
    var method = type.GetMethod("Clear");
    method.Invoke(new object(), null);
}


    
    }



