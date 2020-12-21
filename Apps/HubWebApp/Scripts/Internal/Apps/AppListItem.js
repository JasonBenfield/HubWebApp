"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppListItem = void 0;
var ko = require("knockout");
var AppListItem = /** @class */ (function () {
    function AppListItem(source) {
        this.key = ko.observable('');
        this.title = ko.observable('');
        this.type = ko.observable('');
        this.key(source ? source.AppKey : '');
        this.title(source ? source.Title : '');
        this.type(source ? source.Type.DisplayText : '');
    }
    return AppListItem;
}());
exports.AppListItem = AppListItem;
//# sourceMappingURL=AppListItem.js.map