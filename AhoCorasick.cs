
using System.Collections.Generic;

namespace Bouningen.Lib
{
    // Aho-Corasick法を使って検索・置換を行う
    // 制限事項：Tはintをインデックスにしてアクセス出来るArrayを作れること
    static class AhoCorasick<T>
    {
        public static void convert(TrieTree<T> tree, T[] buf)
        {
            System.Diagnostics.Debug.Assert(tree.root.failure == null);
            for (int i = 0; i < buf.Length; ++i)
            {
                foreach (TrieNode<T> t in tree.root.children)
                {
                    if (t.obj.Equals(buf[i]))
                    {
                        i = search(t, buf, i);
                        break;
                    }
                }
            }
        }

        private static int recusiveDepth = 0;

        public static int search(TrieNode<T> node,  T[] buf,int cur)
        {


            ++recusiveDepth;
            const bool _ = true;
            for (; _; )
            {
            LOOPHEAD:
                // 呼び出し時不変条件：node.obj eq buf[cur]
                // 再帰デバッグ用temporary仕様 node == rootの場合もあり
#if false
            System.Diagnostics.Debug.Assert(node.failure != null);
#endif

                if (node.children.Count == 0)
                {
                    if (node.substitue != null)
                    {
                        replace(node.substitue, buf, cur);
                    }
                    return cur;
                }

                if (cur + 1 >= buf.Length)
                {
                    return cur;
                }

                foreach (TrieNode<T> t in node.children)
                {
                    if (t.obj.Equals(buf[cur + 1]))
                    {
                     //   return search(t, buf, cur + 1);
                        cur++;
                        node = t;
                        goto LOOPHEAD;
                    }
                }

#if false
            if (node.failure.failure != null )
            {
                System.Diagnostics.Debug.Assert(node.obj.Equals( node.failure.obj));
                return search(node.failure, buf, cur);
            }

            return cur;
#else
                if (node.failure == null)
                {
                    break ;
                }

                node = node.failure;

            }

            return cur;
#endif
        }

        public static void replace(Stack<T> rep, T[] buf, int cur)
        {
            while( rep.Count > 0)
            {
                buf[cur] = rep.Pop();
                --cur;
            }
        }
    }
}