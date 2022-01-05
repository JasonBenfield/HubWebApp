"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var PaddingCss_1 = require("@jasonbenfield/sharedwebapp/PaddingCss");
var Startup_1 = require("@jasonbenfield/sharedwebapp/Startup");
var Apis_1 = require("../../Hub/Apis");
var AppListPanel_1 = require("./AppListPanel");
var MainPageView_1 = require("./MainPageView");
var MainPage = /** @class */ (function () {
    function MainPage(page) {
        var view = new MainPageView_1.MainPageView(page);
        this.hubApi = new Apis_1.Apis(page.modalError).hub();
        page.content.setPadding(PaddingCss_1.PaddingCss.top(3));
        this.appListPanel = new AppListPanel_1.AppListPanel(this.hubApi, view.appListPanel);
        this.activateAppListPanel();
    }
    MainPage.prototype.activateAppListPanel = function () {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var result;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        this.appListPanel.refresh();
                        return [4 /*yield*/, this.appListPanel.start()];
                    case 1:
                        result = _a.sent();
                        if (result.appSelected) {
                            this.hubApi.Apps.RedirectToApp.open(result.appSelected.app.ID);
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    return MainPage;
}());
new MainPage(new Startup_1.Startup().build());
//# sourceMappingURL=MainPage.js.map