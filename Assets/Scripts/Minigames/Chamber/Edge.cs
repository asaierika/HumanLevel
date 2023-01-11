using System;

// Directionless via the imposed ordering on assignment of start and endpoint.
public class Edge
{
    private Node startPoint;
    private Node endPoint;

    public Edge(Node firstNode, Node secondNode) {
        if (firstNode.Equals(secondNode)) throw new ArgumentException("Two nodes of an edge should not the same");
        
        if (firstNode.CompareTo(secondNode) < 0) {
            startPoint = firstNode;
            endPoint = secondNode;
        } else {
            startPoint = secondNode;
            endPoint = firstNode;
        }
    }

    public override bool Equals(object other) {
        if (other is Edge) {
            Edge otherEdge = (Edge)other;
            return startPoint == otherEdge.startPoint && endPoint == otherEdge.endPoint;
        }

        return false;
    }

    public override int GetHashCode() {
        return HashCode.Combine<Node, Node>(startPoint, endPoint);
    }
}
