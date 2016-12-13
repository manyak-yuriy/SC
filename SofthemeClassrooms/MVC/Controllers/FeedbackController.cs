using ManagementServices.Implementations;
using ManagementServices.Models;
using System.Web;
using System.Web.Mvc;
using ManagementServices.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly IBusinessLogicFactory _businessLogicFactory;

        public FeedbackController(IBusinessLogicFactory factory)
        {
            _businessLogicFactory = factory;
        }

        public ActionResult SendMessageForm()
        {
            return View();     
        }

        [HttpPost]
        public ActionResult SendMessageForm(SendMessageModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            FeedBackDTO feedback = SendMessageModel.ToFeedbackDTO(model);
            IFeedbackSender sender = _businessLogicFactory.FeedbackSender;
            sender.SendFeedback(feedback);


            return PartialView("SendFeedbackForm", null);
        }
    }
}