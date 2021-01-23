"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourceGroupComponentViewModel = void 0;
var tslib_1 = require("tslib");
var ko = require("knockout");
var template = require("./ResourceGroupComponent.html");
var ComponentTemplate_1 = require("XtiShared/ComponentTemplate");
var Alert_1 = require("XtiShared/Alert");
var ComponentViewModel_1 = require("XtiShared/ComponentViewModel");
var ResourceGroupComponentViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(ResourceGroupComponentViewModel, _super);
    function ResourceGroupComponentViewModel() {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('resource-group-component', template)) || this;
        _this.alert = new Alert_1.AlertViewModel();
        _this.groupName = ko.observable('');
        _this.isAnonymousAllowed = ko.observable(false);
        return _this;
    }
    return ResourceGroupComponentViewModel;
}(ComponentViewModel_1.ComponentViewModel));
exports.ResourceGroupComponentViewModel = ResourceGroupComponentViewModel;
//# sourceMappingURL=ResourceGroupComponentViewModel.js.map