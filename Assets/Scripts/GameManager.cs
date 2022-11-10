using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Proyecto26;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string baseURI;
    public GameObject boxPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("Save Boxes")]
    public void SaveBoxes()
    {
        print("Saving boxes on server");
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Box");
        List<Box> boxes = new List<Box>();

        foreach(GameObject cube in cubes)
        {
            Box myBox = new Box();
            myBox.positionX = cube.transform.position.x;
            myBox.positionY = cube.transform.position.y;
            myBox.positionZ = cube.transform.position.z;

            boxes.Add(myBox);
        }

        //convert to json string and eventually store data on the server
        string jsonBoxes = JsonConvert.SerializeObject(boxes);

        //call the server
        RestClient.Post(baseURI + "api/saveboxes", jsonBoxes).Then(response =>
         {
             print(response.StatusCode.ToString() + " " + response.Text);
         })
        .Catch(err =>
        {
            var error = err as RequestException;
            print(err.Message);
        });

    }
}
