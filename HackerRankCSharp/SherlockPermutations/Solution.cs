using System;

namespace SherlockPermutations {
    class Solution {

        static int []           cache_data  = new int [3] { 1,1,2 };                                    //  initialization must contain at least 1 value (0)!
        static int              cache_size  = 3;                                                     // no big deal in space terms!

        static int              modulo      = (int)  1e9+7;

        public static int [ ] Cache { get => cache_data; set => cache_data= value; }          //

        static Solution ( ) {
            int [] c   = new int [cache_size];
            int    len = Cache.Length;
            Array . Copy ( Cache , c , len );
            for ( int i = len ; i<cache_size ; i++ ) {
                c [ i ]=(int) ( ( (long) c [ i-1 ]*i ) % modulo );
            }
            Cache=c;
        }

        static int fact_mod ( int n ) {

            int len= Cache.Length;

            if ( n < len ) return Cache [ n ];
            else {
                int r=Cache [ len-1 ];
                for ( int i = len ; i <= n ; i++ ) {
                    r = (int) ( ( (long) r * i ) % modulo );
                }
                return r;
            }

        }



        static int modInverse ( int a , int n ) {
            int i = n, v = 0, d = 1;
            while ( a>0 ) {
                int t = i/a, x = a;
                a = i % x;
                i = x;
                x = d;
                d = v - t*x;
                v = x;
            };
            v %= n;
            if ( v<0 ) v =(int) ( (long) ( v+n )%n );
            return v;
        }



        public
        static void Main ( String [ ] args ) {

            int nT = Convert.ToInt32(Console.ReadLine());
            for ( int tn = 0 ; tn < nT ; tn++ ) {
                string[] tokens_n = Console.ReadLine().Split(' ');
                int n = Convert.ToInt32(tokens_n[0]);
                int m = Convert.ToInt32(tokens_n[1]);
                int r,A=0,iB=0;
                if ( m==1 ) r=1;
                else {
                    m-=1;
                    A  = fact_mod ( n+m );
                    iB = modInverse ( (int) ( ( (long) fact_mod ( n ) * fact_mod ( m ) ) %modulo ) , modulo );
                    r = (int) ( ( (long) A * iB )  % modulo );
                }
                Console . WriteLine ( r );
            }
        }
    }
}
