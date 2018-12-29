using System;

namespace MyCollections
{
    public class Node
    {
        public int Index;
        public Object Data;
        public Node NextNode;
    }

    public abstract class Collection
    {
        public abstract Type GetElementType();
        public abstract int GetLength();
    }
}