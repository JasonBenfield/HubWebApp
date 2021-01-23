"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ModifierCategoryListItemViewModel = void 0;
var tslib_1 = require("tslib");
var ko = require("knockout");
var template = require("./ModifierCategoryListItem.html");
var ComponentViewModel_1 = require("XtiShared/ComponentViewModel");
var ComponentTemplate_1 = require("XtiShared/ComponentTemplate");
var ModifierCategoryListItemViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(ModifierCategoryListItemViewModel, _super);
    function ModifierCategoryListItemViewModel(source) {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('mod-category-list-item', template)) || this;
        _this.source = source;
        _this.name = ko.observable('');
        return _this;
    }
    return ModifierCategoryListItemViewModel;
}(ComponentViewModel_1.ComponentViewModel));
exports.ModifierCategoryListItemViewModel = ModifierCategoryListItemViewModel;
//# sourceMappingURL=ModifierCategoryListItemViewModel.js.map