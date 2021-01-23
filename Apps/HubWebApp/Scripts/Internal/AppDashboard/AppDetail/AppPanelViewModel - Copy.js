"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppPanelViewModel = void 0;
var ko = require("knockout");
var AppPanelViewModel = /** @class */ (function () {
    function AppPanelViewModel() {
        this.appName = ko.observable('');
        this.title = ko.observable('');
        this.type = ko.observable('');
    }
    return AppPanelViewModel;
}());
exports.AppPanelViewModel = AppPanelViewModel;
//# sourceMappingURL=AppPanelViewModel.js.map