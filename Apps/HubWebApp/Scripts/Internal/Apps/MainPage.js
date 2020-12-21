"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
require("reflect-metadata");
var xtistart_1 = require("xtistart");
var tsyringe_1 = require("tsyringe");
var MainPageViewModel_1 = require("./MainPageViewModel");
var Alert_1 = require("../../Shared/Alert");
var HubAppApi_1 = require("../../Hub/Api/HubAppApi");
var AppListItem_1 = require("./AppListItem");
var Enumerable_1 = require("../../Shared/Enumerable");
var MainPage = /** @class */ (function () {
    function MainPage(vm, hub) {
        this.vm = vm;
        this.hub = hub;
        this.alert = new Alert_1.Alert(this.vm.alert);
        this.refreshAllApps();
    }
    MainPage.prototype.refreshAllApps = function () {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var apps;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.allApps()];
                    case 1:
                        apps = _a.sent();
                        this.vm.apps(apps);
                        return [2 /*return*/];
                }
            });
        });
    };
    MainPage.prototype.allApps = function () {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var apps;
            var _this = this;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return tslib_1.__awaiter(_this, void 0, void 0, function () {
                            var appsFromSource;
                            return tslib_1.__generator(this, function (_a) {
                                switch (_a.label) {
                                    case 0: return [4 /*yield*/, this.hub.Apps.All()];
                                    case 1:
                                        appsFromSource = _a.sent();
                                        apps = new Enumerable_1.MappedArray(appsFromSource, function (a) { return new AppListItem_1.AppListItem(a); })
                                            .value();
                                        return [2 /*return*/];
                                }
                            });
                        }); })];
                    case 1:
                        _a.sent();
                        return [2 /*return*/, apps];
                }
            });
        });
    };
    MainPage = tslib_1.__decorate([
        tsyringe_1.singleton(),
        tslib_1.__metadata("design:paramtypes", [MainPageViewModel_1.MainPageViewModel,
            HubAppApi_1.HubAppApi])
    ], MainPage);
    return MainPage;
}());
xtistart_1.startup(MainPageViewModel_1.MainPageViewModel, MainPage);
//# sourceMappingURL=MainPage.js.map