using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Cors;

namespace CallCenter.API.Hubs
{
    public class UserHub:Hub
    {
        public void JoinGroup(string group)
        {
            Groups.Add(Context.ConnectionId, group);
        }
        public void SendGroupNotification(string group)
        {
            Clients.Group(group).notify("New user has been added");
        }
        public void CreateNewUser()
        {
            Clients.Caller.notify("DONE");
        }
    }
}