"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ModifierListItemViewModel = void 0;
var tslib_1 = require("tslib");
var ko = require("knockout");
var ComponentTemplate_1 = require("XtiShared/ComponentTemplate");
var ComponentViewModel_1 = require("XtiShared/ComponentViewModel");
var template = require("./ModifierListItem.html");
var ModifierListItemViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(ModifierListItemViewModel, _super);
    function ModifierListItemViewModel() {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('modifier-list-item', template)) || this;
        _this.modKey = ko.observable('');
        _this.displayText = ko.observable('');
        return _this;
    }
    return ModifierListItemViewModel;
}(ComponentViewModel_1.ComponentViewModel));
exports.ModifierListItemViewModel = ModifierListItemViewModel;
//# sourceMappingURL=ModifierListItemViewModel.js.map