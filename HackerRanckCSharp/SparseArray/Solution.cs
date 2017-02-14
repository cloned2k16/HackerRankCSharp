using System;
using System . Collections;

namespace SparseArray {

    class Solution {
        class Counter {
            public int count=1;
            public void inc ( ) {
                count++;
            }
        }

        static void Main ( String [ ] args ) {
            Hashtable dict = new Hashtable();

            int ns = Convert . ToInt32 ( Console . ReadLine ( ) ); ;

            for (int i=0 ; i < ns ; i++ ) {
                string s = Console.ReadLine();
                if ( dict . Contains ( s ) ) ( (Counter) dict [ s ] ) . inc ( );
                else dict [ s ]=new Counter ( );
            }

            int nq = Convert . ToInt32 ( Console . ReadLine ( ) ); 

            for (int i=0 ; i < nq ; i++ ) {
                string qs=Console.ReadLine();
                int r=0;
                if ( dict . Contains ( qs ) ) r= ( (Counter) dict [ qs ] ) . count ;
                Console . WriteLine ( r );

            }


        }
    }
}
