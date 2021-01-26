"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserComponentViewModel = void 0;
var tslib_1 = require("tslib");
var ComponentTemplate_1 = require("XtiShared/ComponentTemplate");
var ComponentViewModel_1 = require("XtiShared/ComponentViewModel");
var Alert_1 = require("XtiShared/Alert");
var template = require("./UserComponent.html");
var ko = require("knockout");
var CommandOutlineButtonTemplate_1 = require("XtiShared/Templates/CommandOutlineButtonTemplate");
var UserComponentViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(UserComponentViewModel, _super);
    function UserComponentViewModel() {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('user-component', template)) || this;
        _this.alert = new Alert_1.AlertViewModel();
        _this.userName = ko.observable('');
        _this.name = ko.observable('');
        _this.email = ko.observable('');
        _this.editCommand = CommandOutlineButtonTemplate_1.createCommandOutlineButtonViewModel();
        return _this;
    }
    return UserComponentViewModel;
}(ComponentViewModel_1.ComponentViewModel));
exports.UserComponentViewModel = UserComponentViewModel;
//# sourceMappingURL=UserComponentViewModel.js.map