using System;
using System.Collections.Generic;
using System.IO;
using RestSharp;
using FreeAgent.Models;

namespace FreeAgent
{
    public class TimeslipClient : ResourceClient<TimeslipWrapper, TimeslipsWrapper, Timeslip>
    {
        public TimeslipClient(FreeAgentClient client) : base(client) {}

        //need to add in the GET to have a parameter for the date filter

        public override string ResouceName { get { return "timeslips"; } } 

        public override TimeslipWrapper WrapperFromSingle(Timeslip single)
        {
            return new TimeslipWrapper { timeslip = single };
        }
        public override IEnumerable<Timeslip> ListFromWrapper(TimeslipsWrapper wrapper)
        {
            return wrapper.timeslips;
        }

        public override Timeslip SingleFromWrapper(TimeslipWrapper wrapper)
        {
            return wrapper.timeslip;
        }

        
        
        
    }
}

