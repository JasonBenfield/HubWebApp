"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserListItemViewModel = void 0;
var tslib_1 = require("tslib");
var ComponentTemplate_1 = require("XtiShared/ComponentTemplate");
var ComponentViewModel_1 = require("XtiShared/ComponentViewModel");
var ko = require("knockout");
var template = require("./UserListItem.html");
var UserListItemViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(UserListItemViewModel, _super);
    function UserListItemViewModel(source) {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('user-list-item', template)) || this;
        _this.source = source;
        _this.userName = ko.observable('');
        _this.name = ko.observable('');
        return _this;
    }
    return UserListItemViewModel;
}(ComponentViewModel_1.ComponentViewModel));
exports.UserListItemViewModel = UserListItemViewModel;
//# sourceMappingURL=UserListItemViewModel.js.map