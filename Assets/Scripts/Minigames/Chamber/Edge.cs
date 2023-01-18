using System;

// Directionless via the imposed ordering on assignment of start and endpoint.
public class Edge
{
    public Node StartPoint { get; private set; }
    public Node EndPoint { get; private set; }

    public Edge(Node firstNode, Node secondNode) {
        if (firstNode.Equals(secondNode)) throw new ArgumentException("Two nodes of an edge should not the same");
        
        if (firstNode.CompareTo(secondNode) < 0) {
            StartPoint = firstNode;
            EndPoint = secondNode;
        } else {
            StartPoint = secondNode;
            EndPoint = firstNode;
        }
    }

    public override bool Equals(object other) {
        if (other is Edge) {
            Edge otherEdge = (Edge)other;
            return StartPoint == otherEdge.StartPoint && EndPoint == otherEdge.EndPoint;
        }

        return false;
    }

    public override int GetHashCode() {
        return HashCode.Combine<Node, Node>(StartPoint, EndPoint);
    }
}
