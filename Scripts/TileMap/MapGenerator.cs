using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    
    public GameManager manager; 
    public GameObject tileGroup; 
    public List<int> BottomLeft;
    public List<int> Left;
    public List<int> TopLeft;
    public List<int> TopRight;
    public List<int> Right;
    public List<int> BottomRight;

    public int[,] gridArray;
    
    private float cellSize;
    private bool indentTile;

    void Awake()
    {
        manager = GameObject.Find("Game Manager").GetComponent<GameManager> ();
        cellSize = 1.3f;
        gridArray = new int[8,8];
        GameObject tileGroup = Instantiate(manager.tileGroup);

        generateMap(gridArray, cellSize, tileGroup);
        
    }

    private void generateMap(int[,] gridArray, float cellSize, GameObject tileGroup) {

        for (int a = 0; a < gridArray.GetLength(0); a++) {

            for (int b = 0; b < gridArray.GetLength(1); b++) {

                GameObject tile = Instantiate(manager.tilePrefabs[Random.Range(0, 5)], GetWorldPosition(a, b), Quaternion.identity);
                indentTile = indentTile ? false : true;

                tile.transform.parent = tileGroup.transform;
                tile.GetComponent<TileProps>().coords.Add(a);
                tile.GetComponent<TileProps>().coords.Add(b);
            }
        }

        tileGroup.transform.position = new Vector3(-4, -4, 0); 
        MakeNeighborhood(tileGroup);

    }

    public Vector3 GetWorldPosition(float x, float y) {

        if (x == 0) {
            x += indentTile ? (cellSize/2) : (0.3f*x);
        }
        else {
            x = indentTile ? (cellSize*x)+(cellSize/2) : (cellSize*x);
        }

        return new Vector3(x, y);
    }

    public void MakeNeighborhood(GameObject Tiles) {

        foreach (Transform tile in Tiles.transform) {
            
            if (tile.GetComponent<TileProps>().isWalkable) {  
                   
                BottomLeft.Clear();
                Left.Clear();
                TopLeft.Clear();
                TopRight.Clear();
                Right.Clear();
                BottomRight.Clear();

                Left.Add(tile.GetComponent<TileProps>().coords[0]-1);
                Left.Add(tile.GetComponent<TileProps>().coords[1]);
                Right.Add(tile.GetComponent<TileProps>().coords[0]+1);
                Right.Add(tile.GetComponent<TileProps>().coords[1]);

                if ((tile.GetComponent<TileProps>().coords[1]%2) == 0) {

                    TopRight.Add(tile.GetComponent<TileProps>().coords[0]);
                    TopRight.Add(tile.GetComponent<TileProps>().coords[1]+1);

                    TopLeft.Add(tile.GetComponent<TileProps>().coords[0]-1);
                    TopLeft.Add(tile.GetComponent<TileProps>().coords[1]+1);                

                    BottomRight.Add(tile.GetComponent<TileProps>().coords[0]);
                    BottomRight.Add(tile.GetComponent<TileProps>().coords[1]-1);

                    BottomLeft.Add(tile.GetComponent<TileProps>().coords[0]-1);
                    BottomLeft.Add(tile.GetComponent<TileProps>().coords[1]-1);

                }
                else {

                    TopRight.Add(tile.GetComponent<TileProps>().coords[0]+1);
                    TopRight.Add(tile.GetComponent<TileProps>().coords[1]+1);

                    TopLeft.Add(tile.GetComponent<TileProps>().coords[0]);
                    TopLeft.Add(tile.GetComponent<TileProps>().coords[1]+1);                

                    BottomRight.Add(tile.GetComponent<TileProps>().coords[0]+1);
                    BottomRight.Add(tile.GetComponent<TileProps>().coords[1]-1);

                    BottomLeft.Add(tile.GetComponent<TileProps>().coords[0]);
                    BottomLeft.Add(tile.GetComponent<TileProps>().coords[1]-1);                

                }

                foreach (Transform tileNeigh in Tiles.transform) {

                    if (tileNeigh.GetComponent<TileProps>().isWalkable) {

                        if (tileNeigh.GetComponent<TileProps>().coords[0] == BottomLeft[0] && tileNeigh.GetComponent<TileProps>().coords[1] == BottomLeft[1]) {
                            
                            tile.GetComponent<TileProps>().neighbours.Add(tileNeigh.GetComponent<TileProps>());
                        }
                        else if (tileNeigh.GetComponent<TileProps>().coords[0] == Left[0] && tileNeigh.GetComponent<TileProps>().coords[1] == Left[1]) {
                            
                            tile.GetComponent<TileProps>().neighbours.Add(tileNeigh.GetComponent<TileProps>());
                        }
                        else if (tileNeigh.GetComponent<TileProps>().coords[0] == TopLeft[0] && tileNeigh.GetComponent<TileProps>().coords[1] == TopLeft[1]) {
                            
                            tile.GetComponent<TileProps>().neighbours.Add(tileNeigh.GetComponent<TileProps>());
                        }
                        else if (tileNeigh.GetComponent<TileProps>().coords[0] == TopRight[0] && tileNeigh.GetComponent<TileProps>().coords[1] == TopRight[1]) {
                            
                            tile.GetComponent<TileProps>().neighbours.Add(tileNeigh.GetComponent<TileProps>());
                        }
                        else if (tileNeigh.GetComponent<TileProps>().coords[0] == Right[0] && tileNeigh.GetComponent<TileProps>().coords[1] == Right[1]) {
                            
                            tile.GetComponent<TileProps>().neighbours.Add(tileNeigh.GetComponent<TileProps>());
                        }
                        else if (tileNeigh.GetComponent<TileProps>().coords[0] == BottomRight[0] && tileNeigh.GetComponent<TileProps>().coords[1] == BottomRight[1]) {
                            
                            tile.GetComponent<TileProps>().neighbours.Add(tileNeigh.GetComponent<TileProps>());
                        }
                    }
                }  
            }          
        }
    }
}
