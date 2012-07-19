﻿using System.Web.Http;
using App.Infrastructure.Web;
using MileageStats.Domain.Handlers;
using MileageStats.Domain.Models;

namespace App.Profile
{
    public class ProfileController : ApiController
    {
        readonly GetUserByClaimId getUser;
        readonly UpdateUser updateUser;

        public ProfileController(GetUserByClaimId getUser, UpdateUser updateUser)
        {
            this.getUser = getUser;
            this.updateUser = updateUser;
        }

        public object GetProfile()
        {
            return new
            {
                name = "",
                countries = new {get = Url.Resource<CountriesController>()},
                save = new {put = Url.Resource<ProfileController>()}
            };
        }

        public void PutProfile()
        {
            //updateUser.Execute(new User { });
        }
    }
}