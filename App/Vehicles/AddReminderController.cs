using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using App.Infrastructure.Web;
using MileageStats.Domain.Contracts;
using MileageStats.Domain.Handlers;

namespace App.Vehicles
{
    public class AddReminderController : ApiController
    {
        readonly AddReminderToVehicle addReminderToVehicle;

        public AddReminderController(AddReminderToVehicle addReminderToVehicle)
        {
            this.addReminderToVehicle = addReminderToVehicle;
        }

        public HttpResponseMessage PostReminder(int id, NewReminder reminder)
        {
            addReminderToVehicle.Execute(1, id, reminder);
            return ReminderCreated(reminder);
        }

        HttpResponseMessage ReminderCreated(NewReminder reminder)
        {
            return new HttpResponseMessage(HttpStatusCode.Created)
            {
                Headers =
                {
                    Location = ReminderUrl(reminder)
                }
            };
        }

        Uri ReminderUrl(NewReminder reminder)
        {
            var url = Url.Resource<ReminderController>(new
            {
                vehicleId = reminder.VehicleId,
                id = reminder.ReminderId
            });
            return new Uri(url, UriKind.Relative);
        }
    }

    public class NewReminder : ICreateReminderCommand
    {
        public int ReminderId { get; set; }
        public int VehicleId { get; set; }
        public string Title { get; set; }
        public string Remarks { get; set; }
        public DateTime? DueDate { get; set; }
        public int? DueDistance { get; set; }
        public bool IsFulfilled { get; set; }
    }
}