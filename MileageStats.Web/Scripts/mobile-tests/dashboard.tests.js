﻿/*  
Copyright Microsoft Corporation

Licensed under the Apache License, Version 2.0 (the "License"); you may not
use this file except in compliance with the License. You may obtain a copy of
the License at 

http://www.apache.org/licenses/LICENSE-2.0 

THIS CODE IS PROVIDED *AS IS* BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
KIND, EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION ANY IMPLIED 
ARRANTIES OR CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE, 
MERCHANTABLITY OR NON-INFRINGEMENT. 

See the Apache 2 License for the specific language governing permissions and
limitations under the License. */

(function (specs, app) {

    module('dashboard specs');

    test('dashboard module constructs itself', function () {
        var module = app.dashboard();

        ok(module != undefined, true);
        equal(typeof module, 'object');
    });

    test('dashboard module should flag an element associated with a vehicle that has an overdue reminder', function () {

        var find_invoked = false;

        var mockView = {
            find: assertion
        };

        var module = app.dashboard();

        module.postrender({ Model: {
            VehicleListViewModel: {
                Vehicles: [{ VehicleId: 1}]
            },
            ImminentReminders: [{ VehicleId: 1}]
        }
        }, mockView);

        function assertion(selector) {
            find_invoked = (selector === '#reminderMenu_1');
            return {
                addClass: function (cssClass) {
                    equal(cssClass, 'flag');
                }
            };
        }

        ok(find_invoked);
    });

    test('dashboard module should only flag an element associated with a particular vehicle once', function () {

        var find_invoked = 0;

        var mockView = {
            find: assertion
        };

        var module = app.dashboard();

        module.postrender({ Model: {
            VehicleListViewModel: {
                Vehicles: [{ VehicleId: 1}]
            },
            ImminentReminders: [{ VehicleId: 1 }, { VehicleId: 1 }, { VehicleId: 1 }]
        }
        }, mockView);

        function assertion(selector) {
            if(selector === '#reminderMenu_1') find_invoked++;
            return { addClass: function () {} };
        }

        equal(find_invoked, 1);
    });


} (window.specs = window.specs || {}, window.mstats));