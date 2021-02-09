"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var xtistart_1 = require("xtistart");
var HubAppApi_1 = require("../../Hub/Api/HubAppApi");
var AppListPanel_1 = require("./AppListPanel");
var PaddingCss_1 = require("XtiShared/PaddingCss");
var MainPage = /** @class */ (function () {
    function MainPage(page) {
        this.page = page;
        this.hubApi = this.page.api(HubAppApi_1.HubAppApi);
        this.appListPanel = this.page.content.addContent(new AppListPanel_1.AppListPanel(this.hubApi));
        this.page.content.setPadding(PaddingCss_1.PaddingCss.top(3));
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
    return MainPage;
}());
new MainPage(new xtistart_1.Startup().build());
//# sourceMappingURL=MainPage.js.map