"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
require("reflect-metadata");
var xtistart_1 = require("xtistart");
var tsyringe_1 = require("tsyringe");
var MainPageViewModel_1 = require("./MainPageViewModel");
var HubAppApi_1 = require("../../Hub/Api/HubAppApi");
var AppListPanel_1 = require("./AppListPanel");
var MainPage = /** @class */ (function () {
    function MainPage(vm, hubApi) {
        this.vm = vm;
        this.hubApi = hubApi;
        this.appListPanel = new AppListPanel_1.AppListPanel(this.vm.appListPanel, this.hubApi);
        this.activateAppListPanel();
    }
    MainPage.prototype.activateAppListPanel = function () {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var result, app;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        this.appListPanel.refresh();
                        return [4 /*yield*/, this.appListPanel.start()];
                    case 1:
                        result = _a.sent();
                        if (result.key === AppListPanel_1.AppListPanel.ResultKeys.appSelected) {
                            app = result.data;
                            this.hubApi.Apps.RedirectToApp.open(app.ID);
                        }
                        return [2 /*return*/];
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