"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ModCategoryPanelViewModel = void 0;
var tslib_1 = require("tslib");
var ComponentTemplate_1 = require("XtiShared/ComponentTemplate");
var CommandOutlineButtonTemplate_1 = require("XtiShared/Templates/CommandOutlineButtonTemplate");
var ComponentViewModel_1 = require("XtiShared/ComponentViewModel");
var template = require("./ModCategoryPanel.html");
var ModCategoryComponentViewModel_1 = require("./ModCategoryComponentViewModel");
var SelectableListCardViewModel_1 = require("../SelectableListCardViewModel");
var ListCardViewModel_1 = require("../ListCardViewModel");
var ModCategoryPanelViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(ModCategoryPanelViewModel, _super);
    function ModCategoryPanelViewModel() {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('mod-category-panel', template)) || this;
        _this.modCategoryComponent = new ModCategoryComponentViewModel_1.ModCategoryComponentViewModel();
        _this.modifierListCard = new ListCardViewModel_1.ListCardViewModel();
        _this.resourceGroupListCard = new SelectableListCardViewModel_1.SelectableListCardViewModel();
        _this.backCommand = CommandOutlineButtonTemplate_1.createCommandOutlineButtonViewModel();
        return _this;
    }
    return ModCategoryPanelViewModel;
}(ComponentViewModel_1.ComponentViewModel));
exports.ModCategoryPanelViewModel = ModCategoryPanelViewModel;
//# sourceMappingURL=ModCategoryPanelViewModel.js.map