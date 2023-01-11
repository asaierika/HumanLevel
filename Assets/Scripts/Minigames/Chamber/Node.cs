using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Two nodes can be linked by an edge
public class Node : MonoBehaviour, IComparable
{
    [SerializeField][Tooltip("Row and column of the node in a grid like gameObject")]
    private Pair<int, int> coordinates;
    [SerializeField]
    private Vector2 targetPosition;
    // To enable related edge adjustment visually when node is dragged about
    private Dictionary<Edge, LineRenderer> outgoingEdges = new Dictionary<Edge, LineRenderer>();
    private Dictionary<Edge, LineRenderer> incomingEdges = new Dictionary<Edge, LineRenderer>();
    public Action<Node> onNodeSelected;
    public Action<Node> onNodeSpecialSelected;

    // Only adjacent nodes (t, d, l, r, tl, tr, bl, br) can be connected
    // NOTE: This makes the solution more attainable and less subjective. Could be modified.
    public static bool IsConnectable(Node firstNode, Node secondNode) {
        if (Mathf.Abs(firstNode.coordinates.head - secondNode.coordinates.head) <= 1 
                && Mathf.Abs(firstNode.coordinates.tail - secondNode.coordinates.tail) <= 1
                && !firstNode.Equals(secondNode)) {
            return true;
        }

        return false;
    }
    
    void OnMouseDown() {
        Debug.Log($"{name} clicked");
        onNodeSelected?.Invoke(this);
        // TODO: UI update of the node
    }

    void OnMouseOver() {
        if (Input.GetMouseButtonDown(1)) onNodeSpecialSelected?.Invoke(this);
    }

    public override bool Equals(object other) {
        if (other is Node) {
            Node otherNode = (Node)other;
            return coordinates.head == otherNode.coordinates.head && coordinates.tail == otherNode.coordinates.tail;
        }

        return false;
    }

    public override int GetHashCode() {
        return HashCode.Combine<int, int>(coordinates.head, coordinates.tail);
    }

    public int CompareTo(object other) {
        if (other == null) return 1;

        Node otherNode = other as Node;
        if (otherNode != null) {
            if (coordinates.head != otherNode.coordinates.head) {
                return coordinates.head.CompareTo(otherNode.coordinates.head);
            } else {
                return coordinates.tail.CompareTo(otherNode.coordinates.tail);
            }
        } else {
            throw new ArgumentException("Object is not a Node");
        }
    }
}
