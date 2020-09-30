"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var MainPageViewModel_1 = require("./MainPageViewModel");
var xtistart_1 = require("xtistart");
var MainPage = /** @class */ (function () {
    function MainPage(viewModel) {
        this.viewModel = viewModel;
    }
    return MainPage;
}());
xtistart_1.startup(function () { return new MainPageViewModel_1.MainPageViewModel(); }, function (vm) { return new MainPage(vm); });
//# sourceMappingURL=MainPage.js.map