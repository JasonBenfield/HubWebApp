"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppListItemViewModel = void 0;
var tslib_1 = require("tslib");
var ko = require("knockout");
var template = require("./AppListItem.html");
var ComponentViewModel_1 = require("XtiShared/ComponentViewModel");
var ComponentTemplate_1 = require("XtiShared/ComponentTemplate");
var AppListItemViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(AppListItemViewModel, _super);
    function AppListItemViewModel(source) {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('app-list-item', template)) || this;
        _this.source = source;
        _this.appName = ko.observable('');
        _this.title = ko.observable('');
        _this.type = ko.observable('');
        _this.url = ko.observable('');
        return _this;
    }
    return AppListItemViewModel;
}(ComponentViewModel_1.ComponentViewModel));
exports.AppListItemViewModel = AppListItemViewModel;
//# sourceMappingURL=AppListItemViewModel.js.map