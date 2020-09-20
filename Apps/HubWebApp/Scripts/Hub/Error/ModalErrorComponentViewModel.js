"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ModalErrorComponentViewModel = void 0;
var ko = require("knockout");
var template = require("./ModalErrorComponent.html");
var ComponentTemplate_1 = require("../ComponentTemplate");
var ModalOptionsViewModel_1 = require("../ModalOptionsViewModel");
var Command_1 = require("../Command");
var CommandButtonTemplate_1 = require("../Templates/CommandButtonTemplate");
var ModalErrorComponentViewModel = /** @class */ (function () {
    function ModalErrorComponentViewModel() {
        this.template = ko.observable('modal-error-component');
        this.title = ko.observable('');
        this.isVisible = ko.observable(false);
        this.modalOptions = new ModalOptionsViewModel_1.ModalOptionsViewModel();
        this.errors = ko.observableArray([]);
        this.okCommand = new Command_1.CommandViewModel();
        new ComponentTemplate_1.ComponentTemplate(this.template(), template).register();
        new CommandButtonTemplate_1.CommandButtonTemplate().register();
    }
    return ModalErrorComponentViewModel;
}());
exports.ModalErrorComponentViewModel = ModalErrorComponentViewModel;
//# sourceMappingURL=ModalErrorComponentViewModel.js.map