"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppListItem = void 0;
var TextBlock_1 = require("@jasonbenfield/sharedwebapp/Html/TextBlock");
var AppListItem = /** @class */ (function () {
    function AppListItem(app, appRedirectUrl, view) {
        this.app = app;
        new TextBlock_1.TextBlock(app.AppName, view.appName);
        new TextBlock_1.TextBlock(app.Title, view.appTitle);
        new TextBlock_1.TextBlock(app.Type.DisplayText, view.appType);
        view.setHref(appRedirectUrl(app.ID));
    }
    return AppListItem;
}());
exports.AppListItem = AppListItem;
//# sourceMappingURL=AppListItem.js.map