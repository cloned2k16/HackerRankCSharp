using System;
using System . Collections;
using System . Collections . Generic;
using System . Diagnostics;

namespace SherlockPairs {
    class Solution {

        // Costrains
        //  1 <= nt   <= 10
        //  1 <= N    <= 1e5
        //  1 <= A[i] <= 1e6


        static long getPairs ( int [] a) {
            long sum = 0;
            int  len = a.Length;
            for ( int i = 1 ; i < len  ; i++ ) {
                long c = 1;
                int p=a[i-1],p1;
                while ( i < len && (p1=a [ i ]) == p ) {
                    c++;
                    p=p1;
                    i++;
                }
                sum +=  (c * ( c-1 )); 
            }
            return sum;
         }

        public
        static void Main ( string [ ] args ) {
            int nt = Convert.ToInt32(Console.ReadLine());
            for ( int t = 0 ; t < nt ; t++ ) {
                int         N   = Convert.ToInt32(Console.ReadLine());
                int []      A   = Array.ConvertAll(Console.ReadLine ().Split(' '),Int32.Parse) ;
                if ( N != A . Length ) Console . WriteLine ( 0 ); //better safe than sorry!

                Array . Sort ( A );
                long res=getPairs ( A );
//                Trace . WriteLine ( String.Format("res: {0}" , res) );
                Console . WriteLine ( res );
            }
        }
    }
}
