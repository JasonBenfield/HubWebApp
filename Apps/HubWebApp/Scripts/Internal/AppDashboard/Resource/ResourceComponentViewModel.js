"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourceComponentViewModel = void 0;
var tslib_1 = require("tslib");
var ko = require("knockout");
var ComponentTemplate_1 = require("XtiShared/ComponentTemplate");
var Alert_1 = require("XtiShared/Alert");
var template = require("./ResourceComponent.html");
var ComponentViewModel_1 = require("XtiShared/ComponentViewModel");
var ResourceComponentViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(ResourceComponentViewModel, _super);
    function ResourceComponentViewModel() {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('resource-component', template)) || this;
        _this.alert = new Alert_1.AlertViewModel();
        _this.resourceName = ko.observable('');
        _this.isAnonymousAllowed = ko.observable(false);
        _this.resultType = ko.observable('');
        return _this;
    }
    return ResourceComponentViewModel;
}(ComponentViewModel_1.ComponentViewModel));
exports.ResourceComponentViewModel = ResourceComponentViewModel;
//# sourceMappingURL=ResourceComponentViewModel.js.map