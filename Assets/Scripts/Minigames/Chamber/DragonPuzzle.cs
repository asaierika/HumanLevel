using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// TODO: Make it playable only in spirit mode
public class DragonPuzzle : Minigame
{
    [SerializeField] private Node[] nodes;
    [SerializeField] private Pair<Node, Node>[] solutionEdgeList;
    // Based on order of edges in solutionEdgeList
    [SerializeField] private List<int> hintEdgeIndexes;
    [SerializeField] private Node currSelectedNode;

    // Fields used for edge drawing
    public Transform edges;
    public LineRenderer edgeRendererPrefab;
    [SerializeField]
    private float drawTime;
    private HashSet<Edge> solutionEdgeSet = new HashSet<Edge>();
    private HashSet<Edge> hintEdgeSet = new HashSet<Edge>();
    private int correctEdgesDrawn = 0;
    private Dictionary<Edge, LineRenderer> drawnEdgeSet = new Dictionary<Edge, LineRenderer>();

    // @param x should be within the range [0, 1]
    // @return progress within the range [0, 1]
    public static float EaseOut(float x) {
        return -Mathf.Pow(x - 1, 2) + 1;
    }

    void Awake() {
        for (int i = 0; i < solutionEdgeList.Length; i++) {
            Pair<Node, Node> edgeDef = solutionEdgeList[i];
            Edge newEdge = new Edge(edgeDef.head, edgeDef.tail);
            solutionEdgeSet.Add(newEdge);
            
            if (hintEdgeIndexes.Contains(i)) {
                hintEdgeSet.Add(newEdge);
            }
        }

        correctEdgesDrawn = hintEdgeIndexes.Count;
    }

    void OnEnable() {
        foreach (Node node in nodes) {
            node.onNodeSelected += TryDrawEdge;
            node.onNodeSpecialSelected += TryRemoveEdge;
        }
    }

    void OnDisable() {
        foreach (Node node in nodes) {
            node.onNodeSelected -= TryDrawEdge;
            node.onNodeSpecialSelected -= TryRemoveEdge;
        }
    }

    void Start() {
        // Generate hint edges
        foreach (Edge hintEdge in hintEdgeSet) {
            LineRenderer edgeRenderer = Instantiate(edgeRendererPrefab, Vector3.zero, Quaternion.identity, edges);
            edgeRenderer.positionCount = 2;
            edgeRenderer.SetPosition(0, hintEdge.StartPoint.transform.position);
            edgeRenderer.SetPosition(1, hintEdge.EndPoint.transform.position);
            drawnEdgeSet.Add(hintEdge, edgeRenderer);
        }
    }

    private void TryDrawEdge(Node nextSelectedNode) {
        if (currSelectedNode != null && Node.IsConnectable(currSelectedNode, nextSelectedNode)) {
            Debug.Log($"Trying to draw and edge from {currSelectedNode.name} + {nextSelectedNode.name}");
            Edge newEdge = new Edge(currSelectedNode, nextSelectedNode);
            if (!drawnEdgeSet.ContainsKey(newEdge)) {
                LineRenderer edgeRenderer = Instantiate(edgeRendererPrefab, Vector3.zero, Quaternion.identity, edges);
                drawnEdgeSet.Add(newEdge, edgeRenderer);
                correctEdgesDrawn = solutionEdgeSet.Contains(newEdge) ? correctEdgesDrawn + 1 : correctEdgesDrawn;
                Debug.Log($"Updated number of target edges yet to be drawn {correctEdgesDrawn}");
                
                StartCoroutine(DrawEdge(currSelectedNode.transform.position, nextSelectedNode.transform.position, edgeRenderer));

                if (correctEdgesDrawn == solutionEdgeSet.Count) {
                    onGameCompleted?.Invoke(null);
                    Debug.Log("Game completed");
                }
            }
        } 

        Debug.Log($"{nextSelectedNode.name} selected");
        currSelectedNode = nextSelectedNode;
    }

    private void TryRemoveEdge(Node nextSelectedNode) {
        if (currSelectedNode != null && Node.IsConnectable(currSelectedNode, nextSelectedNode)) {
            Edge removedEdge = new Edge(currSelectedNode, nextSelectedNode);
            // Edges for hints cannot be removed
            if (drawnEdgeSet.ContainsKey(removedEdge) && !hintEdgeSet.Contains(removedEdge)) {
                Destroy(drawnEdgeSet[removedEdge]);
                correctEdgesDrawn = solutionEdgeSet.Contains(removedEdge) ? correctEdgesDrawn - 1 : correctEdgesDrawn;
                Debug.Log($"Updated number of target edges yet to be drawn {correctEdgesDrawn}");
            }
        }

        currSelectedNode = nextSelectedNode;
    }

    IEnumerator DrawEdge(Vector3 startPoint, Vector3 endPoint, LineRenderer edgeRenderer) {
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
        return (float)correctEdgesDrawn / solutionEdgeSet.Count;
    }

    public override void OnKeyboardExit() {
        MinigameManager.instance.ExitMinigame();
    }
}
