﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TrashCollection.ActionFilters
{    
    public class GlobalRouting : IActionFilter
    {
        private readonly ClaimsPrincipal _claimsPrincipal;
        public GlobalRouting(ClaimsPrincipal claimsPrincipal)
        {
            _claimsPrincipal = claimsPrincipal;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var controller = context.RouteData.Values["controller"];
            if (controller.Equals("home"))
            {
                if (_claimsPrincipal.IsInRole("Customer"))
                {
                    context.Result = new RedirectToActionResult("Index", "Customers", null);
                }
                else if (_claimsPrincipal.IsInRole("Employee"))
                {
                    context.Result = new RedirectToActionResult("Index", "Employees", null);

                }
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
