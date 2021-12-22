"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppListItem = void 0;
var AppListItem = /** @class */ (function () {
    function AppListItem(app, appRedirectUrl, view) {
        this.app = app;
        view.setAppName(app.AppName);
        view.setAppTitle(app.Title);
        view.setAppType(app.Type.DisplayText);
        view.setHref(appRedirectUrl(app.ID));
    }
    return AppListItem;
}());
exports.AppListItem = AppListItem;
//# sourceMappingURL=AppListItem.js.map