using ManagementServices.Implementations;
using ManagementServices.Models;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class FeedbackController : Controller
    {
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
            FeedbackSender sender = new FeedbackSender();
            sender.SendFeedback(feedback);

            return PartialView("SendFeedbackForm", null);
        }
    }
}