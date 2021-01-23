"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ModCategoryComponentViewModel = void 0;
var tslib_1 = require("tslib");
var ko = require("knockout");
var ComponentTemplate_1 = require("XtiShared/ComponentTemplate");
var Alert_1 = require("XtiShared/Alert");
var template = require("./ModCategoryComponent.html");
var Events_1 = require("XtiShared/Events");
var ComponentViewModel_1 = require("XtiShared/ComponentViewModel");
var ModCategoryComponentViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(ModCategoryComponentViewModel, _super);
    function ModCategoryComponentViewModel() {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('resource-group-mod-category-component', template)) || this;
        _this.alert = new Alert_1.AlertViewModel();
        _this.name = ko.observable('');
        _this._clicked = new Events_1.SimpleEvent(_this);
        _this.clicked = _this._clicked.handler();
        return _this;
    }
    ModCategoryComponentViewModel.prototype.click = function () {
        this._clicked.invoke();
    };
    return ModCategoryComponentViewModel;
}(ComponentViewModel_1.ComponentViewModel));
exports.ModCategoryComponentViewModel = ModCategoryComponentViewModel;
//# sourceMappingURL=ModCategoryComponentViewModel.js.map