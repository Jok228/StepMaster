using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StepMaster.Models.API.Day;
using StepMaster.Models.API.UserModel;
using System;
using System.Net;

namespace StepMaster.Auth.AuthRequest
{
    public class ModelValidateFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            if (actionContext.ActionArguments.ContainsKey("dayUpd"))
            {
                foreach (DayResponse item in actionContext.ActionArguments.Values)
                {
                    if (item._id == null) actionContext.Result = new ObjectResult("BadRequest")
                    { StatusCode = (int)HttpStatusCode.BadRequest };
                    if (item.plandistance == null & item.plansteps == null & item.plancalories == null & item.calories == null & item.distance == null & item.steps == null)
                    {
                        actionContext.Result = new ObjectResult("BadRequest")
                        { StatusCode = (int)HttpStatusCode.BadRequest };
                    }
                }
            }
            if (actionContext.ActionArguments.ContainsKey("userEdit"))
            {
                foreach (UserEditModel item in actionContext.ActionArguments.Values)
                {                    
                    if (item.fullname == null & item.nickname == null & item.region_id == null)
                    {
                        actionContext.Result = new ObjectResult("BadRequest")
                        { StatusCode = (int)HttpStatusCode.BadRequest };
                    }
                }
            }


            return;
        }
    }
}
