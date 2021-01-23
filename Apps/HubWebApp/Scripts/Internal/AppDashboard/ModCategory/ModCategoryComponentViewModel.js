"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ModCategoryComponentViewModel = void 0;
var tslib_1 = require("tslib");
var Alert_1 = require("XtiShared/Alert");
var ComponentTemplate_1 = require("XtiShared/ComponentTemplate");
var ComponentViewModel_1 = require("XtiShared/ComponentViewModel");
var ko = require("knockout");
var template = require("./ModCategoryComponent.html");
var ModCategoryComponentViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(ModCategoryComponentViewModel, _super);
    function ModCategoryComponentViewModel() {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('mod-category-component', template)) || this;
        _this.alert = new Alert_1.AlertViewModel();
        _this.name = ko.observable('');
        return _this;
    }
    return ModCategoryComponentViewModel;
}(ComponentViewModel_1.ComponentViewModel));
exports.ModCategoryComponentViewModel = ModCategoryComponentViewModel;
//# sourceMappingURL=ModCategoryComponentViewModel.js.map