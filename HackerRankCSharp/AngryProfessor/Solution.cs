using System;

namespace AngryProfessor {
    class Solution {

        int lateCheck ( string s ) {
            int v=Int32.Parse(s);
            v= v<=0 ? 0 : 1;
            return v;
        }

        public
        static void Main ( String [ ] args ) {
                int t = Convert.ToInt32(Console.ReadLine());
                for ( int a0 = 0 ; a0 < t ; a0++ ) {
                    string[] tokens_n = Console.ReadLine().Split(' ');
                    int n = Convert.ToInt32(tokens_n[0]);
                    int k = Convert.ToInt32(tokens_n[1]);
                    string[] a_temp = Console.ReadLine().Split(' ');

                    int inTime=0;
                    for ( int i = 0 ; i< a_temp . Length ; i++ ) inTime += Int32 . Parse ( a_temp [ i ] ) > 0 ? 0 : 1;
                    if ( inTime >= k ) Console . WriteLine ( "NO" );
                    else Console . WriteLine ( "YES" );

                }
        }
    }
}
