using System;
using System.Collections.Generic;
using System.IO;
using RestSharp;


namespace FreeAgent
{
    public class UserClient : ResourceClient<UserWrapper, UsersWrapper, User>
    {
        public UserClient(FreeAgentClient client) : base(client) {}

        //need to add in the GET to have a parameter for the date filter

        public override string ResouceName { get { return "users"; } } 

        public override UserWrapper WrapperFromSingle(User single)
        {
            return new UserWrapper { user = single };
        }
        public override List<User> ListFromWrapper(UsersWrapper wrapper)
        {
            return wrapper.users;
        }

        public override User SingleFromWrapper(UserWrapper wrapper)
        {
            return wrapper.user;
        }

        public User Me
        {
            get { return Get("me"); }
        }

        
        
        
    }
}

