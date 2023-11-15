using _Project.Develop.Runtime.Engine.UI.MVC.Models;

namespace _Project.Develop.Runtime.Engine.UI.MVC.Controllers
{
    public class BaseController<TModel>
        where TModel : BaseModel
    {
        protected TModel Model;

        public virtual void Setup(TModel model)
        {
            Model = model;
        }
    }
}