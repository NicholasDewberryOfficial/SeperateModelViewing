using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using DialogueEditor;

//NOTE: ONLY WORKS WITH THIS SPECIFIC DATASET
//TO MAKE THIS WORK WITH OTHER DATASETS, I NEED TO CHANGE THE DATA TYPES
//ON SOME CSV FILES THE STRINGS COME FIRST
//IN OTHERS INTS COME FIRST

//UPDATE: 

//to convert this into something I can use for my VR interactivity
//be able to grab object and popup menu shows up where I can look at the specific attributes
//To open a popup menu, there isn't nessecarially a forward way to do it in VR
//most likely will be some strange stuff that i need to get working
//assinging values from the csv file to a dictionary on every object in the scene
//I made this work by using debuglog in the airqualityholder script to visualize the data for each object


//limitations:
// can't deal with csv files where the string columns are in front
//need to individually program a script for every csv file presented to me
//
public class AirQualityPlotter : MonoBehaviour
{

    //********Public Variables********
    // Bools for editor options
    public bool renderPointPrefabs = true;
    public bool renderParticles =  true;
    public bool renderPrefabsWithColor = true;

    // Name of the input file, no extension
    public string inputfile;

    // Indices for columns to be assigned
    //doesnt really matter if public or private
    private int column1 = 0;
    private int column2 = 1;
    private int column3 = 2;
    private int column4 = 3;
    private int column5 = 4;

    public int column6 = 5;

    public int column7 = 6;
    public int column8 = 7;
    public int column9 = 8;
    public int column10 = 9;
    public int column11 = 10;
    public int column12 = 11;


    // Full column names from CSV (as Dictionary Keys)
    //public i want to see these in editor
    public string xColumnName;
    public string yColumnName;
    public string zColumnName;
     
    public string aColumnName;
    public string bColumnName;
    public string cColumnName;

    public string dColumnName;
    public string eColumnName;
    public string fColumnName;
    public string gColumnName;
    public string hColumnName;
    //public string iColumnName;
    

    // Scale of particlePoints within graph, WARNING: Does not scale with graph frame
     private float plotScale = 15;

    // Scale of the prefab particlePoints
    [Range(0.0f, 0.5f)]
    public float pointScale = 0.25f;

    // Changes size of particles generated
    [Range(0.0f, 2.0f)]
    public float particleScale = 5.0f;

    // The prefab for the data particlePoints that will be instantiated
    public GameObject PointPrefab;

    // Object which will contain instantiated prefabs in hiearchy
    public GameObject PointHolder;

    // Color for the glow around the particlePoints
    private Color GlowColor; 
    
    //********Private Variables********
        // Minimum and maximum values of columns
    private float xMin;
    private float yMin;
    private float zMin;

    private float xMax;
    private float yMax;
    private float zMax;

    // Number of rows
    private int rowCount;

    // List for holding data from CSV reader
    private List<Dictionary<string, object>> pointList;

    // Particle system for holding point particles
    private ParticleSystem.Particle[] particlePoints; 


    //********Methods********

    // void Awake()
    // {
    //     //Run CSV Reader
    //     pointList = CSVReader.Read(inputfile);
    // }

    // Use this for initialization
    //void Start () 

    void Awake()
	{
        pointList = CSVReader.Read(inputfile);

        
        // Store dictionary keys (column names in CSV) in a list
        List<string> columnList = new List<string>(pointList[1].Keys);

        Debug.Log("There are " + columnList.Count + " columns in the CSV");

        foreach (string key in columnList)
            Debug.Log("Column name is " + key);

        // Assign column names according to index indicated in columnList
        xColumnName = columnList[column1];
        yColumnName = columnList[column2];
        zColumnName = columnList[column3];
        aColumnName = columnList[column4];
        bColumnName = columnList[column5];
        cColumnName = columnList[column6];
        dColumnName = columnList[column7];
        eColumnName = columnList[column8];
        fColumnName = columnList[column9];
        gColumnName = columnList[column10];
        hColumnName = columnList[column11];
       // iColumnName = columnList[column12];

        
        // Get maxes of each axis, using FindMaxValue method defined below
        xMax = FindMaxValue(xColumnName);
        yMax = FindMaxValue(yColumnName);
        zMax = FindMaxValue(zColumnName);

        // Get minimums of each axis, using FindMinValue method defined below
        xMin = FindMinValue(xColumnName);
        yMin = FindMinValue(yColumnName);
        zMin = FindMinValue(zColumnName);
            
        // Debug.Log(xMin + " " + yMin + " " + zMin); // Write to console

        AssignLabels();

        if (renderPointPrefabs == true)
        {
            // Call PlacePoint methods defined below
            PlacePrefabPoints();
                    }

        // If statement to turn particles on and off
        if ( renderParticles == true)
        {
            // Call CreateParticles() for particle system
            CreateParticles();

            // Set particle system, for point glow- depends on CreateParticles()
            GetComponent<ParticleSystem>().SetParticles(particlePoints, particlePoints.Length);
        }
                                
    }
    
		
	// Update is called once per frame
	void Update ()
    {
        //Activate Particle System
       //GetComponent<ParticleSystem>().SetParticles(particlePoints, particlePoints.Length);

    }

    // Places the prefabs according to values read in
	private void PlacePrefabPoints()
	{
                  
        // Get count (number of rows in table)
        rowCount = pointList.Count;

                for (var i = 0; i < pointList.Count; i++)
        {

            // Set x/y/z, standardized to between 0-1
            float x = (Convert.ToSingle(pointList[i][xColumnName]) - xMin) / (xMax - xMin);
            float y = (Convert.ToSingle(pointList[i][yColumnName]) - yMin) / (yMax - yMin);
            float z = (Convert.ToSingle(pointList[i][zColumnName]) - zMin) / (zMax - zMin);
            string c = (Convert.ToString(pointList[i][cColumnName]));
            // Create vector 3 for positioning particlePoints
			Vector3 position = new Vector3 (x, y, z) * plotScale;

			//instantiate as gameobject variable so that it can be manipulated within loop
			GameObject dataPoint = Instantiate (PointPrefab, Vector3.zero, Quaternion.identity);

            
            // Make child of PointHolder object, to keep particlePoints within container in hiearchy
            dataPoint.transform.parent = PointHolder.transform;

            // Position point at relative to parent
            dataPoint.transform.localPosition = position;

            dataPoint.transform.localScale = new Vector3(pointScale, pointScale, pointScale);

            // Converts index to string to name the point the index number
            string dataPointName = i.ToString();

            // Assigns name to the prefab
            dataPoint.transform.name = dataPointName;
            //assigns tag from the species column
            dataPoint.tag = "cube";
            //add irisdataholder script
            BoxCollider bc = dataPoint.AddComponent<BoxCollider>();
            //add linerenderer
            //PART OF LINERENDERER STRAT - OUTDATED
            // LineRenderer lr = dataPoint.AddComponent<LineRenderer>();
            // lr.material.SetColor("_EmissionColor", new Color(x, y, z, 1.0f));
            bc.isTrigger = true;
           AirQualityHolder Aholder = dataPoint.AddComponent<AirQualityHolder>();
           Aholder.Ozone = System.Convert.ToSingle(pointList[i][xColumnName]);
           Aholder.SolarR = System.Convert.ToSingle(pointList[i][yColumnName]);
           Aholder.Wind = System.Convert.ToSingle(pointList[i][zColumnName]);
           Aholder.Temp = System.Convert.ToSingle(pointList[i][aColumnName]);
           Aholder.Month = System.Convert.ToSingle(pointList[i][bColumnName]);
           Aholder.Day = System.Convert.ToSingle(pointList[i][cColumnName]);
           Aholder.rand1 = System.Convert.ToSingle(pointList[i][dColumnName]);
           Aholder.rand2 = System.Convert.ToSingle(pointList[i][eColumnName]);
           Aholder.rand3 = System.Convert.ToSingle(pointList[i][fColumnName]);
           Aholder.rand4 = System.Convert.ToSingle(pointList[i][gColumnName]);
           Aholder.rand5 = System.Convert.ToSingle(pointList[i][hColumnName]);
           
        //    iHolder.petalLength = System.Convert.ToSingle(pointList[i][aColumnName]);
        //    iHolder.petalWidth = System.Convert.ToSingle(pointList[i][bColumnName]);
        //    iHolder.species = (c);




            if (renderPrefabsWithColor == true)
            {
            //     // Sets color according to x/y/z value
            //     dataPoint.GetComponent<Renderer>().material.color = new Color(x, y, z, 1.0f);

                // sets color according to tag
                // if (dataPoint.tag == "setosa"){
                //     dataPoint.GetComponent<Renderer>().material.color = new Color(1, y, z, 1.0f);
                // } else if (dataPoint.tag == "versicolor"){
                //     dataPoint.GetComponent<Renderer>().material.color = new Color(x, 1, z, 1.0f);  
                // } else if (dataPoint.tag == "virginica"){
                //     dataPoint.GetComponent<Renderer>().material.color = new Color(x, y, 1, 1.0f);

                // }
                // Activate emission color keyword so we can modify emission color
                dataPoint.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");

                dataPoint.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(x, y, z, 1.0f));
                
            }
                                  						
		}

	}

    // creates particlePoints in the Particle System game object
 
 
    private void CreateParticles()
    {

        rowCount = pointList.Count;
       // Debug.Log("Row Count is " + rowCount);

        particlePoints = new ParticleSystem.Particle[rowCount];

        for (int i = 0; i < pointList.Count; i++)
        {
            // Convert object from list into float
            float x = (Convert.ToSingle(pointList[i][xColumnName]) - xMin) / (xMax - xMin);
            float y = (Convert.ToSingle(pointList[i][yColumnName]) - yMin) / (yMax - yMin);
            float z = (Convert.ToSingle(pointList[i][zColumnName]) - zMin) / (zMax - zMin);

            // Debug.Log("Position is " + x + y + z);

            // Set point location
			particlePoints[i].position = new Vector3(x, y, z) * plotScale;
          
            
            // Set point color
            particlePoints[i].startColor = new Color(x, y, z, 1.0f);
            particlePoints[i].startSize = particleScale; 
        }
                
    }

    // Finds labels named in scene, assigns values to their text meshes
    // WARNING: game objects need to be named within scene
    private void AssignLabels()
    {
        // Update point counter
        GameObject.Find("Point_Count").GetComponent<TextMeshPro>().text = pointList.Count.ToString("0");

        // Update title according to inputfile name
        GameObject.Find("Dataset_Label").GetComponent<TextMeshPro>().text = inputfile;

        // Update axis titles to ColumnNames
        GameObject.Find("X_Title").GetComponent<TextMeshPro>().text = xColumnName;
        GameObject.Find("Y_Title").GetComponent<TextMeshPro>().text = yColumnName;
        GameObject.Find("Z_Title").GetComponent<TextMeshPro>().text = zColumnName;

        // // Set x Labels by finding game objects and setting TextMesh and assigning value (need to convert to string)
        // GameObject.Find("X_Min_Lab").GetComponent<TextMesh>().text = xMin.ToString("0.0");
        // GameObject.Find("X_Mid_Lab").GetComponent<TextMesh>().text = (xMin + (xMax - xMin) / 2f).ToString("0.0");
        // GameObject.Find("X_Max_Lab").GetComponent<TextMesh>().text = xMax.ToString("0.0");

        // // Set y Labels by finding game objects and setting TextMesh and assigning value (need to convert to string)
        // GameObject.Find("Y_Min_Lab").GetComponent<TextMesh>().text = yMin.ToString("0.0");
        // GameObject.Find("Y_Mid_Lab").GetComponent<TextMesh>().text = (yMin + (yMax - yMin) / 2f).ToString("0.0");
        // GameObject.Find("Y_Max_Lab").GetComponent<TextMesh>().text = yMax.ToString("0.0");

        // // Set z Labels by finding game objects and setting TextMesh and assigning value (need to convert to string)
        // GameObject.Find("Z_Min_Lab").GetComponent<TextMesh>().text = zMin.ToString("0.0");
        // GameObject.Find("Z_Mid_Lab").GetComponent<TextMesh>().text = (zMin + (zMax - zMin) / 2f).ToString("0.0");
        // GameObject.Find("Z_Max_Lab").GetComponent<TextMesh>().text = zMax.ToString("0.0");
                
    }

    //Method for finding max value, assumes PointList is generated
    private float FindMaxValue(string columnName)
    {
        //set initial value to first value
        float maxValue = Convert.ToSingle(pointList[0][columnName]);

        //Loop through Dictionary, overwrite existing maxValue if new value is larger
        for (var i = 0; i < pointList.Count; i++)
        {
            if (maxValue < Convert.ToSingle(pointList[i][columnName]))
                maxValue = Convert.ToSingle(pointList[i][columnName]);
        }

        //Spit out the max value
        return maxValue;
    }

    //Method for finding minimum value, assumes PointList is generated
    private float FindMinValue(string columnName)
    {
        //set initial value to first value
        float minValue = Convert.ToSingle(pointList[0][columnName]);

        //Loop through Dictionary, overwrite existing minValue if new value is smaller
        for (var i = 0; i < pointList.Count; i++)
        {
            if (Convert.ToSingle(pointList[i][columnName]) < minValue)
                minValue = Convert.ToSingle(pointList[i][columnName]);
        }

        return minValue;
    }

}

