"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourceGroupListItemViewModel = void 0;
var tslib_1 = require("tslib");
var ko = require("knockout");
var template = require("./ResourceGroupListItem.html");
var ComponentTemplate_1 = require("XtiShared/ComponentTemplate");
var ComponentViewModel_1 = require("XtiShared/ComponentViewModel");
var ResourceGroupListItemViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(ResourceGroupListItemViewModel, _super);
    function ResourceGroupListItemViewModel(source) {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('resource-group-list-item', template)) || this;
        _this.source = source;
        _this.name = ko.observable('');
        return _this;
    }
    return ResourceGroupListItemViewModel;
}(ComponentViewModel_1.ComponentViewModel));
exports.ResourceGroupListItemViewModel = ResourceGroupListItemViewModel;
//# sourceMappingURL=ResourceGroupListItemViewModel.js.map