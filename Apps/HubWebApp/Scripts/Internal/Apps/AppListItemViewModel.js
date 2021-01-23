"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppListItemViewModel = void 0;
var ko = require("knockout");
var AppListItemViewModel = /** @class */ (function () {
    function AppListItemViewModel() {
        this.appName = ko.observable('');
        this.title = ko.observable('');
        this.type = ko.observable('');
        this.url = ko.observable('');
    }
    return AppListItemViewModel;
}());
exports.AppListItemViewModel = AppListItemViewModel;
//# sourceMappingURL=AppListItemViewModel.js.map