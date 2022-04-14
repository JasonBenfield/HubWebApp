"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.MainPageView = void 0;
var PaddingCss_1 = require("@jasonbenfield/sharedwebapp/PaddingCss");
var AppListPanelView_1 = require("./AppListPanelView");
var MainPageView = /** @class */ (function () {
    function MainPageView(page) {
        this.page = page;
        this.appListPanel = this.page.content.addContent(new AppListPanelView_1.AppListPanelView());
        this.page.content.setPadding(PaddingCss_1.PaddingCss.top(3));
    }
    return MainPageView;
}());
exports.MainPageView = MainPageView;
//# sourceMappingURL=MainPageView.js.map