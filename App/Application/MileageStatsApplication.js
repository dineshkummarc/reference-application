﻿/// <reference path="~/Infrastructure/Scripts/Vendor/knockout.js"/>
/// <reference path="~/Infrastructure/Scripts/App/Application.js"/>
/// <reference path="~/Infrastructure/Scripts/App/FlashMessage.js"/>

var MileageStatsApplication = Application.inherit({
    
    init: function () {
        Application.init.apply(this, arguments);
        this.flashMessage = FlashMessage.create();
    },
    
    templateId: "Application/MileageStatsApplication.htm"
    
});