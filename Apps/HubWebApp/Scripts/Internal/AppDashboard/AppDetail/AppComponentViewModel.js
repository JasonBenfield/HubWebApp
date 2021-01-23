"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppComponentViewModel = void 0;
var tslib_1 = require("tslib");
var ko = require("knockout");
var Alert_1 = require("XtiShared/Alert");
var ComponentTemplate_1 = require("XtiShared/ComponentTemplate");
var ComponentViewModel_1 = require("XtiShared/ComponentViewModel");
var template = require("./AppComponent.html");
var AppComponentViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(AppComponentViewModel, _super);
    function AppComponentViewModel() {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('app-component', template)) || this;
        _this.alert = new Alert_1.AlertViewModel();
        _this.appName = ko.observable('');
        _this.title = ko.observable('');
        _this.appType = ko.observable('');
        return _this;
    }
    return AppComponentViewModel;
}(ComponentViewModel_1.ComponentViewModel));
exports.AppComponentViewModel = AppComponentViewModel;
//# sourceMappingURL=AppComponentViewModel.js.map