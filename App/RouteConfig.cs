﻿using System.Web.Mvc;
using System.Web.Routing;
using App.Dashboard;
using App.Infrastructure.Web;
using App.Profile;
using App.Specs;
using App.Vehicles;
using App.Vehicles.ReferenceData;
using App.Vehicles.VehicleMasterPage;

namespace App
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.RouteExistingFiles = true;

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapResource<DashboardController>("");
            
            routes.GetResource<VehicleMasterPageController>("vehicles/master");
            routes.GetResource<VehiclesController>("vehicles");
            routes.PostResource<PostVehicleController>("vehicles");
            routes.MapResource<GetNewVehicleController>("vehicles/add");
            routes.GetResource<VehicleController>("vehicles/{id}");
            routes.DeleteResource<DeleteVehicleController>("vehicles/{id}");

            routes.GetResource<FillUpsController>("vehicles/{id}/fillups");
            routes.PostResource<AddFillUpController>("vehicles/{id}/fillups");
            
            routes.GetResource<RemindersController>("vehicles/{id}/reminders");
            routes.PostResource<AddReminderController>("vehicles/{id}/reminders");
            routes.MapResource<ReminderController>("vehicles/{vehicleId}/reminders/{id}");

            routes.MapResource<VehiclePhotoController>("vehicles-photo/{id}");

            routes.MapResource<ProfileController>("profile");

            routes.MapResource<YearsController>("reference/years");
            routes.MapResource<MakesController>("reference/years/{year}");
            routes.MapResource<ModelsController>("reference/years/{year}/{make}");
            routes.MapResource<CountriesController>("reference/countries");


            routes.MapResource<SpecController>("specs");
        }
    }
}