/*  
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

namespace MileageStats.Web.Capabilities
{
    public static class AllCapabilities
    {
        public const string MobileDevice = "isMobileDevice";
        public const string DOMManipulation = "ajax_manipulate_dom";
        public const string JSON = "json";
        public const string HashChange = "hashchange";
        public const string Width = "screenPixelsWidth";
        public const int DefaultWidth = 320;
    }
}
