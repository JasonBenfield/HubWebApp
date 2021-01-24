"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
require("reflect-metadata");
var xtistart_1 = require("xtistart");
var tsyringe_1 = require("tsyringe");
var MainPageViewModel_1 = require("./MainPageViewModel");
var HubAppApi_1 = require("../../Hub/Api/HubAppApi");
var UserListPanel_1 = require("./UserListPanel");
var MainPage = /** @class */ (function () {
    function MainPage(vm, hubApi) {
        this.vm = vm;
        this.hubApi = hubApi;
        this.userListPanel = new UserListPanel_1.UserListPanel(this.vm.userListPanel, this.hubApi);
        this.userListPanel.refresh();
    }
    MainPage = tslib_1.__decorate([
        tsyringe_1.singleton(),
        tslib_1.__metadata("design:paramtypes", [MainPageViewModel_1.MainPageViewModel,
            HubAppApi_1.HubAppApi])
    ], MainPage);
    return MainPage;
}());
xtistart_1.startup(MainPageViewModel_1.MainPageViewModel, MainPage);
//# sourceMappingURL=MainPage.js.map