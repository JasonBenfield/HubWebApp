"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourceListItemViewModel = void 0;
var tslib_1 = require("tslib");
var ko = require("knockout");
var ComponentTemplate_1 = require("XtiShared/ComponentTemplate");
var ComponentViewModel_1 = require("XtiShared/ComponentViewModel");
var template = require("./ResourceListItem.html");
var ResourceListItemViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(ResourceListItemViewModel, _super);
    function ResourceListItemViewModel(source) {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('resource-list-item', template)) || this;
        _this.source = source;
        _this.name = ko.observable('');
        _this.resultType = ko.observable('');
        return _this;
    }
    return ResourceListItemViewModel;
}(ComponentViewModel_1.ComponentViewModel));
exports.ResourceListItemViewModel = ResourceListItemViewModel;
//# sourceMappingURL=ResourceListItemViewModel.js.map