"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserEditPanelViewModel = void 0;
var tslib_1 = require("tslib");
var ComponentTemplate_1 = require("XtiShared/ComponentTemplate");
var ComponentViewModel_1 = require("XtiShared/ComponentViewModel");
var EditUserFormViewModel_1 = require("../../../Hub/Api/EditUserFormViewModel");
var OffscreenSubmitViewModel_1 = require("XtiShared/OffscreenSubmitViewModel");
var panelTemplate = require("./UserEditPanel.html");
var formTemplate = require("./EditUserForm.html");
var Alert_1 = require("XtiShared/Alert");
var CommandButtonTemplate_1 = require("XtiShared/Templates/CommandButtonTemplate");
var UserEditPanelViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(UserEditPanelViewModel, _super);
    function UserEditPanelViewModel() {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('user-edit-panel', panelTemplate)) || this;
        _this.alert = new Alert_1.AlertViewModel();
        _this.editUserForm = new EditUserFormViewModel_1.EditUserFormViewModel();
        _this.offscreenSubmit = new OffscreenSubmitViewModel_1.OffscreenSubmitViewModel();
        _this.saveCommand = CommandButtonTemplate_1.createCommandButtonViewModel();
        _this.cancelCommand = CommandButtonTemplate_1.createCommandButtonViewModel();
        new ComponentTemplate_1.ComponentTemplate('edit-user-form', formTemplate).register();
        _this.editUserForm.componentName('edit-user-form');
        return _this;
    }
    return UserEditPanelViewModel;
}(ComponentViewModel_1.ComponentViewModel));
exports.UserEditPanelViewModel = UserEditPanelViewModel;
//# sourceMappingURL=UserEditPanelViewModel.js.map