using UnityEngine;
using System.Collections;

public class LocationRetrieve : MonoBehaviour
{
    private DataRetrieve dataRetrieve;
    IEnumerator Start()
    {
        Debug.Log("Started");
        //send this guy the location when ready so we can retrieve information about that area from open weather map
        dataRetrieve = GetComponent<DataRetrieve>();

        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("LocationService not enabled");
            yield break;
        }
            

        // Start service before querying location
        Input.location.Start(500);

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
            dataRetrieve.StartConnecting(Input.location.lastData.latitude, Input.location.lastData.longitude);
        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
    }
}