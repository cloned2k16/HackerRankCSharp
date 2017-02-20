using System;

namespace CircularArrayRotation {
    class Solution {
        public
        static void Main ( String [ ] args ) {
            string[]    tokens_n    = Console.ReadLine().Split(' ');
            int         n           = Convert.ToInt32(tokens_n[0]);
            int         k           = Convert.ToInt32(tokens_n[1]);
            int         q           = Convert.ToInt32(tokens_n[2]);
            string[]    a_temp      = Console.ReadLine().Split(' ');
            int[]       A           = Array.ConvertAll(a_temp,Int32.Parse);

            k           =   k % n;                                                      // make sure k falls into array
            int ror     =   (n-k) % n;                                                  // mirror value to rotate right

            int
                i
        ,   ii
        ,   tmp
        ,   ofs     = 0
        ,   cnt     = n
            ;

            while ( 0 < cnt ) {
                i   = ofs;
                tmp = A [ i ];                                      // save first
                do {
                    ii = ( i + ror ) % n;                           // point to new value
                    if ( ii==ofs ) break;                           // until not entering loop (on even length input array)
                    A [ i ] = A [ ii ];                             // move to new place
                    i = ii;                                         // swap index
                    --cnt;
                }
                while ( true );                                     // would be better use ( 0 < cnt ) to keep it safe
                A [ i ]=tmp;                                        // store first value
                --cnt;
                ofs++;                                              // jump to next start
            }

            for ( int a0 = 0 ; a0 < q ; a0++ ) {
                int m = Convert.ToInt32(Console.ReadLine());
                Console . WriteLine ( A [ m ] );
            }
        }
    }
}
