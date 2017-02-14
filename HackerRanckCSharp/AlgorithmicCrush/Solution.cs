using System;

namespace AlgorithmicCrush {
    class Solution {


        public
        static void Main ( String [ ] args ) {

            string [] N_M = Console.ReadLine().Split(' ');

            int N=Convert.ToInt32(N_M[0]);
            int M=Convert.ToInt32(N_M[1]);

            double [] mtx = System.Array.ConvertAll(new double[N+1], v => 0d);


            for (int i=0 ; i < M ; i++ ) {
                string [] a_b_k = Console . ReadLine ( ) . Split ( ' ' );
                int a = Convert.ToInt32(a_b_k[0])-1;  
                int b = Convert.ToInt32(a_b_k[1])-1;
                double  k = Convert.ToDouble(a_b_k[2]);

                // prefix sum ;)
                mtx [ a   ]+=k; 
                mtx [ b+1 ]-=k; 
            }

            double max=mtx [ 0 ];
            for ( int i = 1 ; i<= N ; i++ ) {
                double v=mtx [ i-1 ]+mtx [ i ];
                mtx [ i ]=v;
                if ( v>max ) max=v;
            }

            Console . WriteLine ( max );

        }


    }
}
