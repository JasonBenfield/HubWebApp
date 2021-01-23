"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppListItem = void 0;
var AppListItem = /** @class */ (function () {
    function AppListItem(source, hubApi, vm) {
        vm.appName(source ? source.AppName : '');
        vm.title(source ? source.Title : '');
        vm.type(source ? source.Type.DisplayText : '');
        vm.url(hubApi.Apps.RedirectToApp.getUrl(source.ID).getUrl());
    }
    return AppListItem;
}());
exports.AppListItem = AppListItem;
//# sourceMappingURL=AppListItem.js.map