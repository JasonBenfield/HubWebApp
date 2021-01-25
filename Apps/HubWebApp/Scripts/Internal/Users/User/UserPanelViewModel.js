"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserPanelViewModel = void 0;
var tslib_1 = require("tslib");
var ComponentTemplate_1 = require("XtiShared/ComponentTemplate");
var ComponentViewModel_1 = require("XtiShared/ComponentViewModel");
var CommandOutlineButtonTemplate_1 = require("XtiShared/Templates/CommandOutlineButtonTemplate");
var SelectableListCardViewModel_1 = require("../../ListCard/SelectableListCardViewModel");
var UserComponentViewModel_1 = require("./UserComponentViewModel");
var template = require("./UserPanel.html");
var UserPanelViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(UserPanelViewModel, _super);
    function UserPanelViewModel() {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('user-panel', template)) || this;
        _this.userComponent = new UserComponentViewModel_1.UserComponentViewModel();
        _this.appListCard = new SelectableListCardViewModel_1.SelectableListCardViewModel();
        _this.backCommand = CommandOutlineButtonTemplate_1.createCommandOutlineButtonViewModel();
        return _this;
    }
    return UserPanelViewModel;
}(ComponentViewModel_1.ComponentViewModel));
exports.UserPanelViewModel = UserPanelViewModel;
//# sourceMappingURL=UserPanelViewModel.js.map