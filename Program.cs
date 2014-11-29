using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

using Bouningen.Lib;

namespace binreplace
{
    class Program
    {
        static void Main(string[] args)
        {
            Setting st = new Setting(args[0]);
            st.pattern.makeFailure();
     
            byte[] buf = File.ReadAllBytes(st.target);
            AhoCorasick<byte>.convert(st.pattern, buf);

            File.WriteAllBytes(st.target + @".out", buf);
            File.Replace(st.target + @".out", st.target, st.target + @".old");

/*            fs = File.CreateText( args[0] + ".txt");

//            print_tree(st.pattern.root);
            fs.Write("default(byte) == ");
            fs.WriteLine(default(byte).ToString());
            fs.Flush();
 * 
            convert(st.pattern,buf);
 * */
        }

        /*
     static public TextWriter fs;

        static void print_tree(TrieNode<byte> tree)
        {
            if( tree.obj != default(byte))
            {
                fs.Write("\t");
                fs.Write(tree.obj.ToString("X"));
            }

            if (tree.substitue != null)
            {
                fs.Write("\t");
                foreach (byte b in tree.substitue)
                {
                    fs.Write(b.ToString("x"));
                    fs.Write(" ");
                }
            }

            foreach (TrieNode<byte> t in tree.children)
            {
                print_tree(t);
                fs.Write("\n");
            }

        }

            */
    }
    
    class Setting
        {
            public string target;
            public TrieTree<byte> pattern;

            public Setting(string file)
            {
                using (StreamReader sr = System.IO.File.OpenText(file))
                {
                    target = sr.ReadLine();
                    string s;
                    pattern = new TrieTree<byte>();
                    Regex rex = new Regex(@"[0-9a-zA-Z][0-9a-zA-Z]");
                    while ((s = sr.ReadLine()) != null)
                    {
                        if (s == "" || s[0] == '#')
                            continue;
                        byte b;
                        Match m = rex.Match(s);

                        pattern.setRoot();
                        while( m.Success)
                        {
                            b = 0;
                            b = oct(m.Value[0]);
                            b <<= 4;
                            b += oct(m.Value[1]);
                            pattern.add(ref b);

                            m = m.NextMatch();

                        }
                        s = sr.ReadLine();
                        m = rex.Match(s);
                        while( m.Success )
                        {
                            b = 0;
                            b = oct( m.Value[0] );
                            b <<= 4;
                            b += oct(m.Value[1]);
                            pattern.addSubstitue(b);

                            m = m.NextMatch();
                        }
                    }
                }


            }

            private byte oct(char c)
            {
                switch (c)
                {
                    case '0':
                        return 0;
                    case '1':
                        return 1;
                    case '2':
                        return 2;
                    case '3':
                        return 3;
                    case '4':
                        return 4;
                    case '5':
                        return 5;
                    case '6':
                        return 6;
                    case '7':
                        return 7;
                    case '8':
                        return 8;
                    case '9':
                        return 9;
                    case 'a':
                    case 'A':
                        return 0xA;
                    case 'b':
                    case 'B':
                        return 0xB;
                    case 'c':
                    case 'C':
                        return 0xC;
                    case 'd':
                    case 'D':
                        return 0xD;
                    case 'e':
                    case 'E':
                        return 0xE;
                    case 'f':
                    case 'F':
                        return 0xF;
                    default:
                        throw new FormatException( "not octeat char" );
                }
            }
        }
    




}
