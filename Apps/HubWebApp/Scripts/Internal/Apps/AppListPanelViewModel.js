"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppListPanelViewModel = void 0;
var tslib_1 = require("tslib");
var ComponentViewModel_1 = require("XtiShared/ComponentViewModel");
var ComponentTemplate_1 = require("XtiShared/ComponentTemplate");
var SelectableListCardViewModel_1 = require("../ListCard/SelectableListCardViewModel");
var template = require("./AppListPanel.html");
var AppListPanelViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(AppListPanelViewModel, _super);
    function AppListPanelViewModel() {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('app-list-panel', template)) || this;
        _this.appListCard = new SelectableListCardViewModel_1.SelectableListCardViewModel();
        return _this;
    }
    return AppListPanelViewModel;
}(ComponentViewModel_1.ComponentViewModel));
exports.AppListPanelViewModel = AppListPanelViewModel;
//# sourceMappingURL=AppListPanelViewModel.js.map