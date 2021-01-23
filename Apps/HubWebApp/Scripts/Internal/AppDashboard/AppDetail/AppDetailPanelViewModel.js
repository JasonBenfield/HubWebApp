"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppDetailPanelViewModel = void 0;
var tslib_1 = require("tslib");
var template = require("./AppDetailPanel.html");
var AppComponentViewModel_1 = require("./AppComponentViewModel");
var CurrentVersionComponentViewModel_1 = require("./CurrentVersionComponentViewModel");
var ComponentTemplate_1 = require("XtiShared/ComponentTemplate");
var CommandOutlineButtonTemplate_1 = require("XtiShared/Templates/CommandOutlineButtonTemplate");
var ComponentViewModel_1 = require("XtiShared/ComponentViewModel");
var ListCardViewModel_1 = require("../ListCardViewModel");
var SelectableListCardViewModel_1 = require("../SelectableListCardViewModel");
var AppDetailPanelViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(AppDetailPanelViewModel, _super);
    function AppDetailPanelViewModel() {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('app-detail-panel', template)) || this;
        _this.app = new AppComponentViewModel_1.AppComponentViewModel();
        _this.currentVersion = new CurrentVersionComponentViewModel_1.CurrentVersionComponentViewModel();
        _this.resourceGroupListCard = new SelectableListCardViewModel_1.SelectableListCardViewModel();
        _this.modifierCategoryListCard = new SelectableListCardViewModel_1.SelectableListCardViewModel();
        _this.mostRecentRequestListCard = new ListCardViewModel_1.ListCardViewModel();
        _this.mostRecentErrorEventListCard = new ListCardViewModel_1.ListCardViewModel();
        _this.backCommand = CommandOutlineButtonTemplate_1.createCommandOutlineButtonViewModel();
        return _this;
    }
    return AppDetailPanelViewModel;
}(ComponentViewModel_1.ComponentViewModel));
exports.AppDetailPanelViewModel = AppDetailPanelViewModel;
//# sourceMappingURL=AppDetailPanelViewModel.js.map