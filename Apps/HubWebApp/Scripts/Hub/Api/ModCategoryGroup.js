"use strict";
// Generated code
Object.defineProperty(exports, "__esModule", { value: true });
exports.ModCategoryGroup = void 0;
var tslib_1 = require("tslib");
var AppApiGroup_1 = require("@jasonbenfield/sharedwebapp/Api/AppApiGroup");
var ModCategoryGroup = /** @class */ (function (_super) {
    tslib_1.__extends(ModCategoryGroup, _super);
    function ModCategoryGroup(events, resourceUrl) {
        var _this = _super.call(this, events, resourceUrl, 'ModCategory') || this;
        _this.GetModCategoryAction = _this.createAction('GetModCategory', 'Get Mod Category');
        _this.GetModifiersAction = _this.createAction('GetModifiers', 'Get Modifiers');
        _this.GetModifierAction = _this.createAction('GetModifier', 'Get Modifier');
        _this.GetResourceGroupsAction = _this.createAction('GetResourceGroups', 'Get Resource Groups');
        return _this;
    }
    ModCategoryGroup.prototype.GetModCategory = function (model, errorOptions) {
        return this.GetModCategoryAction.execute(model, errorOptions || {});
    };
    ModCategoryGroup.prototype.GetModifiers = function (model, errorOptions) {
        return this.GetModifiersAction.execute(model, errorOptions || {});
    };
    ModCategoryGroup.prototype.GetModifier = function (model, errorOptions) {
        return this.GetModifierAction.execute(model, errorOptions || {});
    };
    ModCategoryGroup.prototype.GetResourceGroups = function (model, errorOptions) {
        return this.GetResourceGroupsAction.execute(model, errorOptions || {});
    };
    return ModCategoryGroup;
}(AppApiGroup_1.AppApiGroup));
exports.ModCategoryGroup = ModCategoryGroup;
//# sourceMappingURL=ModCategoryGroup.js.map