using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathFinder: MonoBehaviour
{
    [SerializeField]
    private Tilemap tilemap;
    [SerializeField]
    private Grid grid;
    
    public List<Vector3> GetPath(Vector3 from, Vector3 to)
    {
        var cellFrom = grid.WorldToCell(from);
        var cellTo = grid.WorldToCell(to);
        return BFSearch(cellFrom, cellTo)
            .Select(x => grid.CellToWorld(x) + new Vector3(0.5f, 0.5f, 0f))
            .Reverse()
            .ToList();
    }
    
    private List<Vector3Int> BFSearch(Vector3Int from, Vector3Int to)
    {
        var paths = new Dictionary<Vector3Int, Vector3Int>();
        var q = new Queue<Vector3Int>();
        q.Enqueue(from);

        while (q.Count != 0)
        {
            var currentNode = q.Dequeue();
            var adj = GetAdj(currentNode);
            foreach (var pos in adj.Where(x => !paths.ContainsKey(x)))
            {
                q.Enqueue(pos);
                paths.Add(pos, currentNode);
            }
        }
        if (!paths.ContainsKey(to)) Debug.LogError("NoPath");

        var result = new List<Vector3Int>(){to};
        var curNode = to;
        while (curNode != from)
        {
            curNode = paths[curNode];
            result.Add(curNode);
        }

        return result;
    }

    private List<Vector3Int> GetAdj(Vector3Int tilePos)
    {
        var result = new List<Vector3Int>();
        
        var right = new Vector3Int(tilePos.x + 1, tilePos.y, tilePos.z);
        var up = new Vector3Int(tilePos.x, tilePos.y + 1, tilePos.z);
        var left = new Vector3Int(tilePos.x - 1, tilePos.y, tilePos.z);
        var down = new Vector3Int(tilePos.x, tilePos.y - 1, tilePos.z);
        
        if(tilemap.GetTile(right) != null) result.Add(right);
        if(tilemap.GetTile(up) != null) result.Add(up);
        if(tilemap.GetTile(left) != null) result.Add(left);
        if(tilemap.GetTile(down) != null) result.Add(down);

        return result;
    }
}
