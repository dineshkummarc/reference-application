﻿/// <reference path="~/Infrastructure/Scripts/App/Object.js"/>

var Vehicle = Object.inherit({

    init: function (pageData) {
        this.name = pageData.name;
        this.year = pageData.year;
        this.make = pageData.make;
        this.model = pageData.model;
        this.odometer = pageData.odometer;
        this.photo = pageData.photo;
    },
    
    templateId: "Vehicles/VehiclePage/Vehicle.htm"
});