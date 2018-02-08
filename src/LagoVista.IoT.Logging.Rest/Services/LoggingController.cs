using LagoVista.Core.Models.UIMetaData;
using LagoVista.IoT.Logging.Loggers;
using LagoVista.IoT.Logging.Models;
using LagoVista.IoT.Web.Common.Attributes;
using LagoVista.IoT.Web.Common.Controllers;
using LagoVista.UserAdmin.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LagoVista.IoT.Logging.Rest.Services
{

    [ConfirmedUser]
    [Authorize]
    public class LoggingController : LagoVistaBaseController
    {
        ILogReader _logReader;

        public LoggingController(ILogReader reader, UserManager<AppUser> userManager, IAdminLogger logger) : base(userManager, logger)
        {
            _logReader = reader;
        }

        [HttpGet("/api/logging/errors")]
        public Task<ListResponse<LogRecord>> GetAllErrorsAsync()
        {
            return _logReader.GetAllErrorsAsync(GetListRequestFromHeader());
        }
    }
}
