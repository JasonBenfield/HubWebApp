"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.CurrentVersionComponentViewModel = void 0;
var tslib_1 = require("tslib");
var ko = require("knockout");
var Alert_1 = require("XtiShared/Alert");
var ComponentTemplate_1 = require("XtiShared/ComponentTemplate");
var ComponentViewModel_1 = require("XtiShared/ComponentViewModel");
var template = require("./CurrentVersionComponent.html");
var CurrentVersionComponentViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(CurrentVersionComponentViewModel, _super);
    function CurrentVersionComponentViewModel() {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('current-version-component', template)) || this;
        _this.alert = new Alert_1.AlertViewModel();
        _this.versionKey = ko.observable('');
        _this.version = ko.observable('');
        return _this;
    }
    return CurrentVersionComponentViewModel;
}(ComponentViewModel_1.ComponentViewModel));
exports.CurrentVersionComponentViewModel = CurrentVersionComponentViewModel;
//# sourceMappingURL=CurrentVersionComponentViewModel.js.map