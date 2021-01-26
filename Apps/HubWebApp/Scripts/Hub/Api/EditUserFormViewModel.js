"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.EditUserFormViewModel = void 0;
// Generated code
var ko = require("knockout");
var InputFieldViewModel_1 = require("XtiShared/Forms/InputFieldViewModel");
var HiddenFieldViewModel_1 = require("XtiShared/Forms/HiddenFieldViewModel");
var EditUserFormViewModel = /** @class */ (function () {
    function EditUserFormViewModel() {
        this.componentName = ko.observable('EditUserForm');
        this.UserID = new HiddenFieldViewModel_1.HiddenFieldViewModel();
        this.PersonName = new InputFieldViewModel_1.InputFieldViewModel();
        this.Email = new InputFieldViewModel_1.InputFieldViewModel();
    }
    return EditUserFormViewModel;
}());
exports.EditUserFormViewModel = EditUserFormViewModel;
//# sourceMappingURL=EditUserFormViewModel.js.map