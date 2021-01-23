"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.RoleAccessListItemViewModel = void 0;
var tslib_1 = require("tslib");
var ko = require("knockout");
var template = require("./RoleAccessListItem.html");
var ComponentTemplate_1 = require("XtiShared/ComponentTemplate");
var ComponentViewModel_1 = require("XtiShared/ComponentViewModel");
var RoleAccessListItemViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(RoleAccessListItemViewModel, _super);
    function RoleAccessListItemViewModel() {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('role-access-list-item', template)) || this;
        _this.roleName = ko.observable('');
        _this.isAllowed = ko.observable(false);
        return _this;
    }
    return RoleAccessListItemViewModel;
}(ComponentViewModel_1.ComponentViewModel));
exports.RoleAccessListItemViewModel = RoleAccessListItemViewModel;
//# sourceMappingURL=RoleAccessListItemViewModel.js.map