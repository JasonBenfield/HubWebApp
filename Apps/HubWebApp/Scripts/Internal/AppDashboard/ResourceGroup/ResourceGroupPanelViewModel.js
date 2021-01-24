"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourceGroupPanelViewModel = void 0;
var tslib_1 = require("tslib");
var template = require("./ResourceGroupPanel.html");
var ComponentTemplate_1 = require("XtiShared/ComponentTemplate");
var SelectableListCardViewModel_1 = require("../../ListCard/SelectableListCardViewModel");
var ResourceGroupComponentViewModel_1 = require("./ResourceGroupComponentViewModel");
var CommandOutlineButtonTemplate_1 = require("XtiShared/Templates/CommandOutlineButtonTemplate");
var ComponentViewModel_1 = require("XtiShared/ComponentViewModel");
var ModCategoryComponentViewModel_1 = require("./ModCategoryComponentViewModel");
var ListCardViewModel_1 = require("../../ListCard/ListCardViewModel");
var ResourceGroupPanelViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(ResourceGroupPanelViewModel, _super);
    function ResourceGroupPanelViewModel() {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('resource-group-panel', template)) || this;
        _this.resourceGroupComponent = new ResourceGroupComponentViewModel_1.ResourceGroupComponentViewModel();
        _this.modCategoryComponent = new ModCategoryComponentViewModel_1.ModCategoryComponentViewModel();
        _this.roleAccessCard = new ListCardViewModel_1.ListCardViewModel();
        _this.resourceListCard = new SelectableListCardViewModel_1.SelectableListCardViewModel();
        _this.mostRecentRequestListCard = new ListCardViewModel_1.ListCardViewModel();
        _this.mostRecentErrorEventListCard = new ListCardViewModel_1.ListCardViewModel();
        _this.backCommand = CommandOutlineButtonTemplate_1.createCommandOutlineButtonViewModel();
        return _this;
    }
    return ResourceGroupPanelViewModel;
}(ComponentViewModel_1.ComponentViewModel));
exports.ResourceGroupPanelViewModel = ResourceGroupPanelViewModel;
//# sourceMappingURL=ResourceGroupPanelViewModel.js.map