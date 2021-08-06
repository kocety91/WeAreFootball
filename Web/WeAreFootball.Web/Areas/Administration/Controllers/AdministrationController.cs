namespace WeAreFootball.Web.Areas.Administration.Controllers
{
    using WeAreFootball.Common;
    using WeAreFootball.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
