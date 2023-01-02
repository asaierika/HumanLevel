using System;

[Serializable]
public struct Pair<U, T> {
    public Pair(U head, T tail) {
        this.head = head;
        this.tail = tail;
    }
    public U head;
    public T tail;
}
