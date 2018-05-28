using       System;
using       System.Diagnostics;
using       System.IO;
using       System.Reflection;
using       System.Threading;

namespace                               yaoo                        {



    class                               Program 
    :                                   ObjBase                     {

        void                            doTest                      ( string pckgName, int [] testCases  )              {
            
            string me = Process.GetCurrentProcess().MainModule.FileName;

            OUT ( "Testing '{0}' [{1}]" , pckgName , string.Join(",",testCases));
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
                    OUT ( line );
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
                            OUT ( "EXCEPTION: {0}",eIn.Message );
                            errAny=true;
                        }

                    } ) . Start ( );


                    int ln=1;
                    using ( StreamReader outF = new StreamReader ( "..\\..\\"+pckgName+"\\output"+tcN+".txt" ) ) {
                        while ( outF . Peek ( ) >= 0 ) {
                            string ok   = outF . ReadLine ( );
                            string res  = outP . ReadLine ( );
                            if ( res != ok ) {
                                OUT ( "Error at @{0} {1} != {2} !" , ln , res , ok );
                            }
                            ln++;
                        }
                    }
    
                }
                catch (Exception e) {
                    OUT ( string.Format( "EXCEPTION: {0}" , e.Message ));
                    errAny=true;
                }

                if ( !errAny ) {
                    OUT ( "# {0} Done." , testCases [ tc ] );
                }
                else {
                    OUT ( "# {0} ERROR." , testCases [ tc ] );
                }
                p . WaitForExit ( );
                p . Close ( );
            }



        }

        public override
        void                            Init                        (String [] args)                                    {
            
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
                OUT ( "{0}" , String . Join ( "," , args ) );
                string testPackage = args[0];


                OUT ( "ready" );

                try {
                    string      className   = testPackage+".Solution";
                    Type        type        = Type.GetType(className);
                    object      obj         = Activator.CreateInstance(type);

                    MethodInfo  mthd        = type.GetMethod("Main");
                    mthd . Invoke ( obj , new object [ ] { args } );
                }
                catch ( Exception e ) {
                    OUT ( string.Format("Exception: {0} " , e ));
                }

            }
        }

        static 
        void                            Main                        (String [] args )                                   {  newInstance().Init(args);  }

    }
}