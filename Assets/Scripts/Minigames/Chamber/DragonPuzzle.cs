using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonPuzzle : Minigame
{
    [SerializeField]
    private Node[] nodes;
    [SerializeField]
    private Pair<Node, Node>[] solutionEdgeList;
    [SerializeField]
    private Node lastSelectedNode;

    // Fields used for edge drawing
    public Transform edges;
    public LineRenderer edgeRendererPrefab;
    [SerializeField]
    private float drawTime;
    // Edges in the solution yet to be drawn.
    private HashSet<Edge> solutionEdgeSet = new HashSet<Edge>();
    private HashSet<Edge> drawnEdgeSet = new HashSet<Edge>();

    // @param x should be within the range [0, 1]
    // @return progress within the range [0, 1]
    public static float EaseOut(float x) {
        return -Mathf.Pow(x - 1, 2) + 1;
    }

    void Awake() {
        foreach (Pair<Node, Node> edgeDef in solutionEdgeList) {
            solutionEdgeSet.Add(new Edge(edgeDef.head, edgeDef.tail));
        }
    }

    void OnEnable() {
        foreach (Node node in nodes) {
            node.OnNodeSelected += TryDrawEdge;
        }
    }

    void OnDisable() {
        foreach (Node node in nodes) {
            node.OnNodeSelected -= TryDrawEdge;
        }
    }

    private void TryDrawEdge(Node newNode) {
        if (lastSelectedNode != null) {
            Debug.Log($"Trying to draw and edge from {lastSelectedNode.name} + {newNode.name}");
            if (Node.IsConnectable(lastSelectedNode, newNode)) {
                Edge newEdge = new Edge(lastSelectedNode, newNode);
                if (!drawnEdgeSet.Contains(newEdge)) {
                    drawnEdgeSet.Add(newEdge);
                    solutionEdgeSet.Remove(newEdge);
                    
                    StartCoroutine(DrawEdge(lastSelectedNode.transform.position, newNode.transform.position));

                    if (solutionEdgeSet.Count == 0) {
                        onGameCompleted?.Invoke(null);
                    }
                }
            }
        } 

        Debug.Log($"{newNode.name} selected");
        lastSelectedNode = newNode;
    }

    IEnumerator DrawEdge(Vector3 startPoint, Vector3 endPoint) {
        LineRenderer edgeRenderer = Instantiate(edgeRendererPrefab, Vector3.zero, Quaternion.identity, edges);
        edgeRenderer.positionCount = 2;
        edgeRenderer.SetPosition(0, startPoint);
        edgeRenderer.SetPosition(1, startPoint);
        
        float timeElapsed = 0;
        while (timeElapsed / drawTime <= 1) {
            edgeRenderer.SetPosition(1, (endPoint - startPoint) * EaseOut(timeElapsed / drawTime) + startPoint);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    public override float GetProgress() {
        return (float)solutionEdgeSet.Count / solutionEdgeList.Length;
    }

    public override void OnKeyboardExit() {
        throw new System.NotImplementedException();
    }
}
