"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var MainPageViewModel_1 = require("./MainPageViewModel");
var TelephoneNumber_1 = require("../TelephoneNumber");
var cpwstart_1 = require("cpwstart");
var MainPage = /** @class */ (function () {
    function MainPage(viewModel) {
        this.viewModel = viewModel;
        this.viewModel.telephoneNumber(new TelephoneNumber_1.TelephoneNumber(864, 555, 1234).toString());
    }
    return MainPage;
}());
cpwstart_1.startup(function () { return new MainPageViewModel_1.MainPageViewModel(); }, function (vm) { return new MainPage(vm); });
//# sourceMappingURL=MainPage.js.map