using System.Net;
using System.Net.Mail;
using System.Text;
using CandyShop.Domain.Abstract;
using CandyShop.Domain.Entities;
using System;

namespace CandyShop.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = "dyadinmaxim1@gmail.com";
        public string MailFromAddress = "sweetstorebibd@gmail.com";
        public bool UseSsl = true;
        public string Username = "sweetstorebibd@gmail.com";
        public string Password = "1q2a3z2w";
        public string ServerName = "smtp.gmail.com";
        public int ServerPort = 587;
        public bool WriteAsFile = true;
        public string FileLocation = @"D:\university\bibd\kurs\eml";
    }

    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;

        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }

        public void ProcessOrder(Cart cart, ShippingDetails shippingInfo)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials
                    = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod
                        = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                    .AppendLine("Новый заказ обработан")
                    .AppendLine("---")
                    .AppendLine("Товары:");

                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Sweet.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (итого: {2:c}",
                        line.Quantity, line.Sweet.Name, subtotal);
                }

                body.AppendFormat("Общая стоимость: {0:c}", cart.ComputeTotalValue())
                    .AppendLine("---")
                    .AppendLine("Доставка:")
                    .AppendLine(shippingInfo.Name)
                    .AppendLine(shippingInfo.Line1)
                    .AppendLine(shippingInfo.Line2 ?? "")
                    .AppendLine(shippingInfo.Line3 ?? "")
                    .AppendLine(shippingInfo.City)
                    .AppendLine(shippingInfo.Country)
                    .AppendLine("---")
                    .AppendFormat("Подарочная упаковка: {0}",
                        shippingInfo.GiftWrap ? "Да" : "Нет");

                MailMessage mailMessage = new MailMessage(
                                       emailSettings.MailFromAddress,	// От кого
                                       emailSettings.MailToAddress,		// Кому
                                       "Новый заказ отправлен!",		// Тема
                                       body.ToString()); 				// Тело письма

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.UTF8;
                }

                smtpClient.Send(mailMessage);

                Byer byer = new Byer();
                byer.Name = shippingInfo.Name;
                byer.Email = "test@gmail.com";
                byer.Adress = shippingInfo.Line1;

                EFByerRepository repoB = new EFByerRepository();
                repoB.SaveByer(byer);
                EFOrderRepository repoO = new EFOrderRepository();

                foreach (var line in cart.Lines)
                {
                    Order order = new Order();
                    order.IdB = byer.Id;
                    order.IdS = line.Sweet.Id;
                    repoO.SaveOrder(order);
                }
            }
        }
    }
}