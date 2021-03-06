﻿/*  
Copyright Microsoft Corporation

Licensed under the Apache License, Version 2.0 (the "License"); you may not
use this file except in compliance with the License. You may obtain a copy of
the License at 

http://www.apache.org/licenses/LICENSE-2.0 

THIS CODE IS PROVIDED ON AN *AS IS* BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
KIND, EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION ANY IMPLIED 
WARRANTIES OR CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE, 
MERCHANTABLITY OR NON-INFRINGEMENT. 

See the Apache 2 License for the specific language governing permissions and
limitations under the License. */

(function (specs, app) {

    module('reminder fullfill specs');

    test('reminder fullfill module constructs itself', function () {
        var module = app.reminderFulfill(mocks.create({ formSubmitter: { attach: function () {
        }
        }}));

        ok(module != undefined, true);
        equal(typeof module, 'object');
    });


    test('reminder fullfill module should invoke formSubmitter with the correct arguments', function () {

        expect(2);

        var m = mocks.create({
            formSubmitter: {
                attach: function (el, callback) {
                    equal(typeof el, 'object');
                    equal(typeof callback, 'function');
                }
            }
        });

        var module = app.reminderFulfill(m);
        module.postrender({}, m.$('view'), {});

    });

} (window.specs = window.specs || {}, window.mstats));