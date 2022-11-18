using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// TODO: Remove this class if follower works.
public class Navigation : MonoBehaviour
{
    public GameObject target;
    public Stack<Vector2> route;
    public Tilemap walkable;
    public int rows;
    public int cols;
    protected bool[,] visited;
    private Rigidbody2D npcBody;
    public float npcSpeed;
    public float acceptedDistMargin;
    public float acceptedTargetProximity;
    public Vector2 nextWayPoint; 
    public Vector2Int offset;
    public bool reached;
    public bool needRouteUpdate;

    void Start() {
        route = new Stack<Vector2>();
        npcBody = GetComponent<Rigidbody2D>();
        visited = new bool[cols, rows];
        nextWayPoint = Vector2.positiveInfinity;
    }

    void Update() {
        // if (Input.GetMouseButtonDown(0)) {
        //     Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //     Debug.Log($"Mouse click at {worldPosition}");
        //     Vector3Int roundedPosition = new Vector3Int(Mathf.RoundToInt(worldPosition.x), Mathf.RoundToInt(worldPosition.y), 0);
        //     Debug.Log(walkable.HasTile(roundedPosition));
        //     Vector3Int cellPosition = walkable.WorldToCell(roundedPosition);
        //     Debug.Log($"Cell position {cellPosition}");
        //     Debug.Log(walkable.HasTile(cellPosition));
        // }
        
        if (Vector3.Distance(transform.position, target.transform.position) <= acceptedTargetProximity) {
            Debug.Log("Dest reached");
            reached = true;
            nextWayPoint = Vector2.positiveInfinity;
            route.Clear();
        }
    }

    void FixedUpdate()
    {
        if (!reached) {
            if (needRouteUpdate) {
                // For moving target
                route.Clear();
                visited = new bool[cols, rows];
                GetRoute(walkable.WorldToCell(transform.position));
                route.Pop();
                nextWayPoint = route.Pop();
                needRouteUpdate = false;
            }

            if (nextWayPoint.Equals(Vector2.positiveInfinity) && route.Count == 0) {
                GetRoute(walkable.WorldToCell(transform.position));
                // Ignore first waypoint which represents start point 
                route.Pop();
                nextWayPoint = route.Pop();
            } else if (Vector2.Distance(nextWayPoint, transform.position) <= acceptedDistMargin) {
                nextWayPoint = route.Pop();
                Debug.Log($"Moving towards {nextWayPoint}");
            }

            npcBody.MovePosition(npcBody.position + Time.deltaTime * (Vector2) Vector3.Normalize(nextWayPoint - npcBody.position) * npcSpeed);
        } else if (Vector3.Distance(transform.position, target.transform.position) > acceptedTargetProximity * 2) {
            reached = false;
        }
    }

    // void OnCollisionEnter2D(Collision2D other) {
    //     if (GameObject.ReferenceEquals(other.gameObject, target)) {
    //         Debug.Log("Dest reached");
    //         reached = true;
    //         nextWayPoint = Vector2.positiveInfinity;
    //         route.Clear();
    //     }
    // }

    // void OnCollisionExit2D(Collision2D other) {
    //      if (GameObject.ReferenceEquals(other.gameObject, target)) {
    //         reached = false;
    //      }
    // }

    /**
        A custom implementation of DFS.
        Shorten distance to target as priority, but under each scenario all possible directions must still be enumerated
    **/
    public bool GetRoute(Vector3Int current) {
        if (visited[current.x - offset.x, current.y - offset.y]) {
            return false;
        }

        Debug.Log($"Visiting {current}");
        visited[current.x - offset.x, current.y - offset.y] = true;
        Vector3Int targetCellPosition = walkable.WorldToCell(target.transform.position);

        if (current.Equals(targetCellPosition)) {
            Debug.Log("Found a route");
            Vector2 worldPoint = walkable.CellToWorld(current);
            route.Push(new Vector2(Mathf.Ceil(worldPoint.x), Mathf.Ceil(worldPoint.y)));
            return true;
        } else {
            if (walkable.HasTile(current)) {
                bool inRoute = false;
                Debug.Log("Possible path through this cell");
               
                if (current.x != targetCellPosition.x) {
                    Vector3Int xDir = new Vector3Int(targetCellPosition.x - current.x > 0 ? 1 : -1, 0);
                    Vector3Int yDir = new Vector3Int(0, targetCellPosition.y - current.y > 0 ? 1 : -1);
                    inRoute = GetRoute(current + xDir) 
                        || GetRoute(current + yDir)
                        || GetRoute(current - xDir)
                        || GetRoute(current - yDir);
                } else {
                    Vector3Int yDir = new Vector3Int(0, targetCellPosition.y - current.y > 0 ? 1 : -1);
                    inRoute = GetRoute(current + yDir) 
                        || GetRoute(current + Vector3Int.right) 
                        || GetRoute(current + Vector3Int.left)
                        || GetRoute(current - yDir);
                }

                if (inRoute) {
                    Vector2 worldPoint = walkable.CellToWorld(current);
                    Vector2 roundedPoint = new Vector2(Mathf.Ceil(worldPoint.x), Mathf.Ceil(worldPoint.y));
                    route.Push(roundedPoint);
                    return true;
                }

                return false;
            }

            Debug.Log("No tile here");
            return false;
        }
    }
}
