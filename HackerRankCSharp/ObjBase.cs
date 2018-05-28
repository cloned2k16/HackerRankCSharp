
using System;
using System.Diagnostics;

namespace                               yaoo                        {

    class                               ObjBase                     
        :                               IProgram                    {

        public
        void                            OUT                         (string fmt, params object [] args)                 {
            SOUT(fmt,args);
        }

        public static
        void                            SOUT                        (string fmt, params object [] args)                 {
            Console.WriteLine(fmt,args);
        }

        public static 
        IProgram                        newInstance                 () {
            Type clss=new StackTrace(1).GetFrame(0).GetMethod().DeclaringType;
            string name=clss.ToString();
            return (IProgram) Activator.CreateInstance(clss);   
        }

        virtual
        public void                  Init                        ( string [] args )                                  {   throw new NotImplementedException ();   }
    }

}
