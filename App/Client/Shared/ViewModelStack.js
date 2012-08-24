﻿/// <reference path="UrlStack.js" />
/// <reference path="http.js"/>

var ViewModelStack = UrlStack.inherit({
    
    init: function (app, http) {
        this.app = app;
        this.http = http;
        this.viewModels = {};

        UrlStack.init.call(this, this.downloadUrl, this.disposeViewModel);
    },
    
    downloadUrl: function (url) {
        return this.http({ method: "GET", url: url });
    },

    navigate: function (newUrl) {
        var navigation = UrlStack.navigate.call(this, newUrl);
        navigation.done(function (commonParentUrl) {
            if (commonParentUrl) {
                // The parent view model has not changed, but we've loaded a new child.
                // So we need to set the parent's content property.
                this.updateParentContent(commonParentUrl);
            }
        });
        return navigation;
    },
    
    updateParentContent: function (parentUrl) {
        // Child URL will be after the parent URL in the this.urls array.
        var childUrl = this.urls[1 + this.urls.indexOf(parentUrl)];
        var childViewModel = this.viewModels[childUrl];
        if (childViewModel) {
            var parentViewModel = this.viewModels[parentUrl];
            parentViewModel.content(childViewModel);
        }
    },
    
    processDownloadResponse: function (response) {
        // Call prototype
        var process = UrlStack.processDownloadResponse.apply(this, arguments);

        var downloadModule = function () {
            // Use require.js to download the view model module.
            var moduleResult = $.Deferred();
            var modulePath = response.body.Script;
            require([modulePath], function (module) {
                moduleResult.resolveWith(this, [module]);
            }.bind(this));
            return moduleResult;
        }.bind(this);
        
        var createViewModel = function (module) {
            var viewData = response.body.Data;
            var viewModel = module.init(viewData, this.app);
            this.viewModels[response.url] = viewModel;
            return viewModel;
        }.bind(this);

        var setViewModelContent = function(viewModel) {
            if (response.childUrl) {
                var childViewModel = this.viewModels[response.childUrl];
                viewModel.content(childViewModel);
            }
        }.bind(this);
        
        return process
            .pipe(downloadModule)
            .pipe(createViewModel)
            .pipe(setViewModelContent)
            .pipe(function () { return response; });
    },
    
    disposeViewModel: function (url) {
        if (this.viewModels[url]) {
            if (typeof this.viewModels[url].dispose === "function") {
                this.viewModels[url].dispose();
            }
            delete this.viewModels[url];
        }
    },
    
    rootViewModel: function () {
        return this.viewModels[this.urls[0]];
    }
});