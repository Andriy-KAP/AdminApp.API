using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Http;

namespace CallCenter.API.Controllers
{
    public class ControllerBase: ApiController
    {
        protected int GetCurrentUserGroupId()
        {
            int groupId;
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            if (int.TryParse(identity.Claims.ToArray()[2].Value, out groupId))
                return groupId;
            return -1;
        }
    }
}