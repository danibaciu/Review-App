using System.Collections.Generic;
using Core.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;


namespace ReviewApp.Controllers.Api
{
    public class BaseController : Controller
    {
        private IAuthService _authService;

        public IAuthService _AuthService
        {
            get { return _authService ?? HttpContext.RequestServices.GetService<IAuthService>(); }
            set { _authService = value; }
        }

        //----------------------------

        private IPreferenceService _preferenceService;

        public IPreferenceService _PreferenceService
        {
            get { return _preferenceService ?? HttpContext.RequestServices.GetService<IPreferenceService>(); }
            set { _preferenceService = value; }
        }

        private IProfileService _profileService;

        public IProfileService _ProfileService
        {
            get { return _profileService ?? HttpContext.RequestServices.GetService<IProfileService>(); }
            set { _profileService = value; }
        }

        private IRolesService _rolseService;

        public IRolesService _RoleService
        {
            get { return _rolseService ?? HttpContext.RequestServices.GetService<IRolesService>(); }
            set { _rolseService = value; }
        }

        private IArticleTypeService _articleTypeService;

        public IArticleTypeService _ArticleTypeService
        {
            get { return _articleTypeService ?? HttpContext.RequestServices.GetService<IArticleTypeService>(); }
            set { _articleTypeService = value; }
        }

        private IArticleService _articleService;

        public IArticleService _ArticleService
        {
            get { return _articleService ?? HttpContext.RequestServices.GetService<IArticleService>(); }
            set { _articleService = value; }
        }

        private ICommentService _commentService;

        public ICommentService _CommentService
        {
            get { return _commentService ?? HttpContext.RequestServices.GetService<ICommentService>(); }
            set { _commentService = value; }
        }
    }
}
