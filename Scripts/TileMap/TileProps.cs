using System;
using System.Collections;
using System.Collections.Generic;
using PathFinding;
using UnityEngine;

public class TileProps : MonoBehaviour, IAStarNode
{

    private Pathfinding _pathfinding;


    public IEnumerable<IAStarNode> Neighbours => neighbours;
    public List<TileProps> neighbours = new List<TileProps>();

    public SpriteRenderer _spriterenderer;
    public float cost;
    public bool isWalkable;
    public bool pathGenerated;
    public List<int> coords = new List<int>();

    private float timer;

    void Awake()
    {
        _pathfinding = GameObject.Find("Pathfinding").GetComponent<Pathfinding> ();
        _spriterenderer = GetComponent<SpriteRenderer>();

    }

    void Update() {
        
        if (pathGenerated) {
            timer += Time.deltaTime;
        }

        if (timer > 4f && pathGenerated) {
            _spriterenderer.color = Color.white;
            timer = 0f;
            pathGenerated = false;
        }
    }

    public void OnMouseDown()
    {
        _pathfinding.TileClicked(this);
    }

    public float CostTo(IAStarNode neighbour)
    {
        return cost;
    }

    public float EstimatedCostTo(IAStarNode goal)
    {
        Transform goalTransform = ((Component)GetComponent<IAStarNode>()).transform;

        int estimated = (Math.Abs(this.GetComponent<TileProps>().coords[0]) - Math.Abs(goalTransform.GetComponent<TileProps>().coords[0])) + 
                        (Math.Abs(this.GetComponent<TileProps>().coords[1]) - Math.Abs(goalTransform.GetComponent<TileProps>().coords[1]));

        return estimated;
    }    
}
