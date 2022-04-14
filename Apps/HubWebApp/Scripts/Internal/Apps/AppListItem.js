"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppListItem = void 0;
var TextBlock_1 = require("@jasonbenfield/sharedwebapp/Html/TextBlock");
var AppListItem = /** @class */ (function () {
    function AppListItem(appWithModKey, appRedirectUrl, view) {
        this.appWithModKey = appWithModKey;
        new TextBlock_1.TextBlock(appWithModKey.App.AppName, view.appName);
        new TextBlock_1.TextBlock(appWithModKey.App.Title, view.appTitle);
        new TextBlock_1.TextBlock(appWithModKey.App.Type.DisplayText, view.appType);
        view.setHref(appRedirectUrl(appWithModKey.ModKey));
    }
    return AppListItem;
}());
exports.AppListItem = AppListItem;
//# sourceMappingURL=AppListItem.js.map