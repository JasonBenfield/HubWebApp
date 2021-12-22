"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var UserPage_1 = require("@jasonbenfield/sharedwebapp/User/UserPage");
var Startup_1 = require("@jasonbenfield/sharedwebapp/Startup");
var Apis_1 = require("../../Hub/Apis");
var pageFrame = new Startup_1.Startup().build();
new UserPage_1.UserPage(pageFrame, new Apis_1.Apis(pageFrame.modalError).hub());
//# sourceMappingURL=MainPage.js.map