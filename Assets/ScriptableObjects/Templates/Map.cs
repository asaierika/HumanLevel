using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Map")]
public class Map : ScriptableObject
{
    // Start and end locations must exactly match scene name.
    public List<string> startLocations;
    public List<string> endLocations;
    public List<Vector2> startingPositions;
    public Dictionary<string, Vector2> lookupTable = new Dictionary<string, Vector2>();

    public void InitDictionary() {
        if (startLocations == null || endLocations == null || startingPositions == null 
                || !(startLocations.Count == endLocations.Count && startLocations.Count == startingPositions.Count)) {
            throw new UnityException("The edges provided for the map is invalid");
        } 

        if (lookupTable.Count == 0) {
            Debug.Log("Generating map edges.");
            for (int i = 0; i < startLocations.Count; i++) {
                lookupTable[startLocations[i] + endLocations[i]] = startingPositions[i];
            }
        }
    }

    public Vector2 GetStartingPosition(string startLocation, string endLocation) {
        if (lookupTable.ContainsKey(startLocation + endLocation)) {
            Debug.Log("Spawn Location at " + endLocation + " " + lookupTable[startLocation + endLocation]);
            return lookupTable[startLocation + endLocation];
        }
        throw new UnityException("The " + name + " map does not contain the given edge.");
    }
}
