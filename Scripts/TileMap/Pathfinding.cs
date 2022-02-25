using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFinding;
using System.Linq;

public class Pathfinding : MonoBehaviour
{
    private TileProps start;
    private TileProps goal;

    private IEnumerable<TileProps> drawPath;

    public IEnumerable<TileProps> GetPath(TileProps start, TileProps goal)
    {
        var path = AStar.GetPath(start, goal);
        
        if (path == null || path.Count == 0) {
            return null;
        }

        drawPath = path.Cast<TileProps>();
        ColorPath(start, goal);
        
        return drawPath;
    }

    public void TileClicked(TileProps tileprops)
    {
        if (start == null && tileprops.isWalkable) {
            start = tileprops;
            tileprops._spriterenderer.color = Color.green;
        } 
        else if (goal == null && tileprops.isWalkable) {
            goal = tileprops;
            tileprops._spriterenderer.color = Color.green;
        }
        else {

            if (tileprops.isWalkable) {

                start = tileprops;
                goal = null;
            }
        }

        if (goal != null && start != null) {
            GetPath(start, goal);
            goal = null;
            start = null;
        }
    }

    private void ColorPath(IAStarNode start, IAStarNode goal)
    {
        if (drawPath == null) {
            return;
        }
        foreach (TileProps tile in drawPath) {
            
            tile.pathGenerated = true;
            if (tile != start && tile != goal) {
                tile._spriterenderer.color = Color.red;
            }
        }
    }
}
