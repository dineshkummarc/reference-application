using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using App.Infrastructure.Web;
using MileageStats.Domain.Handlers;

namespace App.Vehicles.ReferenceData
{
    public class MakesController : ApiController
    {
        readonly GetYearsMakesAndModels getYearsMakesAndModels;

        public MakesController(GetYearsMakesAndModels getYearsMakesAndModels)
        {
            this.getYearsMakesAndModels = getYearsMakesAndModels;
        }

        public IEnumerable<object> GetMakes(int year)
        {
            var makes = getYearsMakesAndModels.Execute(year).Item2;

            return makes.Select(make => new
            {
                make,
                models = Url.Get<ModelsController>(new {year, make})
            });
        }
    }
}