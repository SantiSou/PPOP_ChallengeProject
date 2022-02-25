using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject tileGroup;
    public GameObject[] tilePrefabs;
    public Camera orthographicCamera;
    
    private void Awake() {
        
        Debug.Log("Initializing manager...");

    }
}
