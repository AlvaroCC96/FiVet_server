using System;

namespace Fivet.ZeroIce
{
    class Program
    {/*
 * Created on Sun May 17 2020
 *
 * Copyright (c) 2020 Your Company
 */

        public static int Main(string[] args)
        {   /**
            * Server waiting connections 
            */
            try
            {
                using(Ice.Communicator communicator = Ice.Util.initialize(ref args))
                {   
                    //TODO: testing connections , failed with Java Client
                    var adapter = communicator.createObjectAdapterWithEndpoints("TheAdapter","default -p 8080 -z");
                    adapter.add(new TheSystemImpl(), Ice.Util.stringToIdentity("TheSystem"));
                    adapter.activate();
                    communicator.waitForShutdown();
                }
            }
            catch(Exception e)
            {
                Console.Error.WriteLine(e);
                return 1;
            }
            return 0;
        }
    }

    /**
    * Implementation of Interface TheSystem
    */
    class TheSystemImpl : model.TheSystemDisp_ {
         public override long getDelay(long clientTime, Ice.Current current)
        {
            return DateTime.Now.Ticks - clientTime;
        }
    }
}
