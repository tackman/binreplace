
using System.Collections.Generic;

namespace Bouningen.Lib
{
    class TrieNode<T>
    {
        public TrieNode()
        {
            children = new Stack<TrieNode<T>>();
            substitue = null;
            failure = null;
        }
        public Stack<TrieNode<T>> children;
        public T obj;
        public TrieNode<T> failure;
        public Stack<T> substitue; // 置換後パターン
    }

    class TrieTree<T>
    {
        public TrieTree()
        {
            root = new TrieNode<T>();
            cur = root;
        }

        public TrieNode<T> root;
        private TrieNode<T> cur;

        public void add(ref T x, TrieNode<T> node)
        {
            cur = node;
            add(ref x);
        }

        public void setRoot()
        {
            cur = root;
        }


        public void add(ref T x)
        {
            TrieNode<T> tmp = new TrieNode<T>();
            tmp.obj = x;

            foreach (TrieNode<T> t in cur.children)
            {
                if (t.obj.Equals( x ))
                {
                    cur = t;
                    return;
                }
            }

            cur.children.Push(tmp);
            cur = cur.children.Peek();
            return;

        }


        public void makeFailure()
        {
            foreach (TrieNode<T> t in root.children)
            {
                makeFailure(t, root);
            }
        }
        public void makeFailure(TrieNode<T> now, TrieNode<T> goTo)
        {
            now.failure = goTo;
            foreach (TrieNode<T> t in now.children)
            {
                foreach (TrieNode<T> u in goTo.children)
                {
                    if (t.obj.Equals(u.obj))
                    {
                        makeFailure(t, u);
                        break;
                    }
                }

                if (t.failure == null)
                {
                    makeFailure(t, root);
                }
            }
        }

        public void addSubstitue(T t)
        {
            if (null == cur.substitue)
            {
                cur.substitue = new Stack<T>();
            }
            cur.substitue.Push(t);
        }

    }
}