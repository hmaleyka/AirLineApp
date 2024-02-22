using Airline.Business.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using static QRCoder.PayloadGenerator;

namespace Airline.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    [AutoValidateAntiforgeryToken]
    public class BarcodeController : Controller
    {
        [Authorize(Roles = "SuperAdmin, Admin , Moderator")]
        public IActionResult Index()
        {
            QRCodeVM model = new();
            return View(model);
        }
        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpPost]
        public IActionResult Index(QRCodeVM model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}
            Payload? payload = null;
            switch (model.QRCodeType)
            {
                case 1: // compose sms
                    payload = new SMS(model.SMSPhoneNumber, model.SMSBody);
                    break;
                case 2: // compose whatsapp message
                    payload = new WhatsAppMessage(model.WhatsAppNumber, model.WhatsAppMessage);
                    break;
                case 3: //compose email
                    payload = new Mail(model.ReceiverEmailAddress, model.EmailSubject, model.EmailMessage);
                    break;
                case 4: // wifi qr code
                    payload = new WiFi(model.WIFIName, model.WIFIPassword, WiFi.Authentication.WPA);
                    break;
            }

            QRCodeGenerator qrGenerator = new();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(payload);
            BitmapByteQRCode qrCode = new(qrCodeData);
            string base64String = Convert.ToBase64String(qrCode.GetGraphic(20));
            model.QRImageURL = "data:image/png;base64," + base64String;
            return View("Index", model);
        }
    }
}
