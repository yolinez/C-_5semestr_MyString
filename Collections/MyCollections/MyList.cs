using System;

namespace MyCollections
{
    public class MyList<T> : Collection
    {
        public int Length => _length;
        public Object First => _root.Data;

        private Node _root;
        private T _type;
        private int _length = 0;

        public MyList()
        {
            CreateMyList(ref _root);
            _length = 0;
        }

        public MyList(int size)
        {
            CreateMyList(ref _root, size);
            _length = size;
        }

        public Object this[int index] { 
            get=>FindNodeByIndex(index, _root).Data;
            set => FindNodeByIndex(index, _root).Data = value;
        }

        public void Add(Object data)
        {
            _length++;
            var root = _root;
            if (root == null){_root = new Node(){Data = data};return;}
            while (true)
            {
                if (root.NextNode == null)
                {
                    root.NextNode = new Node(){Index = root.Index+1,Data = data};
                    return;
                }
                root = root.NextNode;
            }
        }
        
        public void AddRange(int size)
        {
            var root = _root;
            while (root.NextNode != null)
            {
                root = root.NextNode;
            }

            for (int i = 0; i < size; i++)
            {
                root.NextNode = new Node(){Data = _type,Index = root.Index+1};
                root = root.NextNode;
                _length++;
            }
        }
        
        private void CreateMyList(ref Node root) => root = null;

        private void CreateMyList(ref Node root,int size,int index = 0)
        {
            root = new Node(){Index = index,Data = _type};
            index++;
            if(index<size)CreateMyList(ref root.NextNode,size,index);
        }

        private Node FindNodeByIndex(int index,Node root)
        {
            if (root.Index == index) return root;
            if (root.NextNode == null) return null;
            return FindNodeByIndex(index,root.NextNode);
        }
        
        public override int GetLength() => Length;
        public override Type GetElementType() => _type.GetType();
    }
}