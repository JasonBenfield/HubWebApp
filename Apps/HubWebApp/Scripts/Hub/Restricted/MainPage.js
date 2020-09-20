"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var MainPageViewModel_1 = require("./MainPageViewModel");
var cpwstart_1 = require("cpwstart");
var MainPage = /** @class */ (function () {
    function MainPage(viewModel) {
        this.viewModel = viewModel;
    }
    return MainPage;
}());
cpwstart_1.startup(function () { return new MainPageViewModel_1.MainPageViewModel(); }, function (vm) { return new MainPage(vm); });
//# sourceMappingURL=MainPage.js.map