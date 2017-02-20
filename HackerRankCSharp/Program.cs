using System;
using System . Diagnostics;
using System . IO;
using System . Reflection;
using System . Threading;

class Injector {
    Injector ( Type clss ) {
     Object theObject = Activator.CreateInstance(clss);
     
    }
}

class Program {

    static void doTest ( string pckgName, int [] testCases  ) {
        string me = Process.GetCurrentProcess().MainModule.FileName;

        Console . WriteLine ( "Testing '{0}' [{1}]" , pckgName , string.Join(",",testCases));
        for ( int tc = 0 ; tc < testCases . Length ; tc++ ) {
            string tcN = testCases[tc].ToString("00");

                Process p = new Process();
            p . StartInfo . FileName                = me;
            p . StartInfo . Arguments               = pckgName;
            p . StartInfo . UseShellExecute         = false;
            p . StartInfo . RedirectStandardInput   = true;
            p . StartInfo . RedirectStandardOutput  = true;
            p . Start ( );

            StreamWriter inP  = p.StandardInput;
            StreamReader outP = p.StandardOutput;
            //StreamReader errP = p.StandardError;

        
            string line;
            do {
                line= outP . ReadLine ( );
                Console . WriteLine ( line );
            }
            while ( line != "ready" );

            bool errAny = false;

            try {

                new Thread ( ( ) => {
                    Thread . CurrentThread . IsBackground = false;
                    try {
                        using ( StreamReader inF = new StreamReader ( "..\\..\\"+pckgName+"\\input"+tcN+".txt" ) ) {
                            while ( inF . Peek ( ) >= 0 ) {
                                inP . WriteLine ( inF . ReadLine ( ) );
                            }
                        }
                    }
                    catch ( Exception eIn ) {
                        Console . WriteLine ( "EXCEPTION: {0}",eIn.Message );
                        errAny=true;
                    }

                } ) . Start ( );


                int ln=1;
                using ( StreamReader outF = new StreamReader ( "..\\..\\"+pckgName+"\\output"+tcN+".txt" ) ) {
                      while ( outF . Peek ( ) >= 0 ) {
                        string ok   = outF . ReadLine ( );
                        string res  = outP . ReadLine ( );
                        if ( res != ok ) {
                                Console . WriteLine ( "Error at @{0} {1} != {2} !" , ln , res , ok );
                            }
                            ln++;
                      }
                }
    
            }
            catch (Exception e) {
                Console . WriteLine ( string.Format( "EXCEPTION: {0}" , e.Message ));
                errAny=true;
            }

            if ( !errAny ) {
                Console . WriteLine ( "# {0} Done." , testCases [ tc ] );
            }
            else {
                Console . WriteLine ( "# {0} ERROR." , testCases [ tc ] );
            }
            p . WaitForExit ( );
            p . Close ( );
        }



    }

    static void Main ( String [ ] args ) {
        Debug . Listeners . Add ( new TextWriterTraceListener ( Console .Error ) );

        if ( args . Length==0 ) {

            doTest ( "AngryProfessor"           , new int [ ] { 66      } );
            doTest ( "CircularArrayRotation"    , new int [ ] { 66      } );
            doTest ( "AlgorithmicCrush"         , new int [ ] {  7      } );
            doTest ( "SherlockPermutations"     , new int [ ] {  0      } );
            doTest ( "SherlockPairs"            , new int [ ] {  0,  3  } );

            
            Console . ReadKey ( );

        }
        else {
            Console . WriteLine ( "{0}" , String . Join ( "," , args ) );
            string testPackage = args[0];


            Console . WriteLine ( "ready" );

            try {
                string      className   = testPackage+".Solution";
                Type        type        = Type.GetType(className);
                object      obj         = Activator.CreateInstance(type);

                MethodInfo  mthd        = type.GetMethod("Main");
                mthd . Invoke ( obj , new object [ ] { args } );
            }
            catch ( Exception e ) {
                Debug . WriteLine ( string.Format("Exception: {0} " , e ));
            }

        }
    }

}