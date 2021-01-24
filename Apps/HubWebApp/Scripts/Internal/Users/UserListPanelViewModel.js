"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserListPanelViewModel = void 0;
var tslib_1 = require("tslib");
var ComponentTemplate_1 = require("XtiShared/ComponentTemplate");
var ComponentViewModel_1 = require("XtiShared/ComponentViewModel");
var SelectableListCardViewModel_1 = require("../ListCard/SelectableListCardViewModel");
var template = require("./UserListPanel.html");
var UserListPanelViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(UserListPanelViewModel, _super);
    function UserListPanelViewModel() {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('user-list-panel', template)) || this;
        _this.userListCard = new SelectableListCardViewModel_1.SelectableListCardViewModel();
        return _this;
    }
    return UserListPanelViewModel;
}(ComponentViewModel_1.ComponentViewModel));
exports.UserListPanelViewModel = UserListPanelViewModel;
//# sourceMappingURL=UserListPanelViewModel.js.map