using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class testlocation : MonoBehaviour
{
    public Text latText;
    public Text lonText;

    private float lat;
    private float lon;

    public void Start()
    {

        // turn on location services, if available 
        Input.location.Start(10,0.1f);
    }

    public void Update()
    {
        //Text singleText = GameObject.Find("SinglePlayerButton").GetComponentInChildren<Text>();

        //Do nothing if location services are not available

       

        if (Input.location.isEnabledByUser)
        {
            lat = Input.location.lastData.latitude;
            lon = Input.location.lastData.longitude;
            latText.text = lat.ToString();
            lonText.text = lon.ToString();
            //singleText.text = "Depart lat: " + lat + "lon: " + lon;

        }
        else
        {
            latText.text = "gps off";
            lonText.text = "gps off";
        }
           

    }



}