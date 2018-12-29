using System;
using System.Xml.Serialization.Configuration;

namespace MyCollections
{    
    public class MyStack<T>:Collection
    {
        public int Length => _length;
        
        private Node _root;
        private T _type;
        private int _length = 0; 

        public MyStack()
        {
            _root = null;
        }

        public MyStack(int size)
        {
            Create(ref _root,size);
            _length = size;
        }

        public Object Peek()
        {
            if(_root!=null)return _root.Data;
            return null;
        }

        public void Push(Object data)
        {
            _length++;
            if(_root == null){_root = new Node(){Data = data};
                return;
            }
            var newNode = new Node(){Data = data,NextNode = _root};
            _root = newNode;
            _length++;
        }

        public Object Pop()
        {
            _length--;
            var saveNode = _root;
            _root = _root.NextNode;
            return saveNode.Data;
        }
        
        private void Create(ref Node root, int size,int index = 0)
        {
            if (index >= size) return;
            var newNode = new Node(){Data = _type,NextNode = root};
            root = newNode;
            index++;
            Create(ref root,size,index);
        }
        
        public override Type GetElementType() => _type.GetType();
        public override int GetLength() => 0;
    }
}