using System;

namespace MyCollections
{  
    class MyArray<T>:Collection
    {
        public int Length { get; }

        private readonly Node _root;
        private T _type;
  
        public MyArray(int size)
        {
            CreateArray(ref _root,size);
            Length = size;
        }

        public MyArray(T[] array)
        {
            CreateArray(ref _root,array);
            Length = array.Length;
        }

        public Object this[int index]
        {
            get=>FindNodeByIndex(index, _root).Data;
            set => FindNodeByIndex(index, _root).Data = value;
        } 

        public void Clear()
        {
            var root = _root;
            while (root != null)
            {
                root.Data = _type;
                root = root.NextNode;
            }
        }

        public bool Exist(Object data)
        {
            var root = _root;
            while (root != null)
            {
                if (root.Data.Equals(data)) return true;
                root = root.NextNode;
            }

            return false;
        }
        
        private void CreateArray(ref Node root,int size,int index = 0)
        {
            root = new Node {Index = index,Data = _type};
            index++;
            if(index<size)CreateArray(ref root.NextNode,size,index);
        }
        private void CreateArray(ref Node root,T[] array,int index = 0)
        {            
            root = new Node {Index = index,Data = array[index]};
            index++;
            if(index<array.Length)CreateArray(ref root.NextNode,array,index);
        }
        
        private Node FindNodeByIndex(int index,Node root)
        {
            if (root.Index == index) return root;
            if (root.NextNode == null) return null;
            return FindNodeByIndex(index,root.NextNode);
        }
        
        public override Type GetElementType() => _type.GetType();
        public override int GetLength() => Length;
    }
}