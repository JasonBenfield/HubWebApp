"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ModalErrorComponentViewModel = void 0;
var tslib_1 = require("tslib");
var ko = require("knockout");
var template = require("./ModalErrorComponent.html");
var ComponentTemplate_1 = require("../ComponentTemplate");
var ModalOptionsViewModel_1 = require("../ModalOptionsViewModel");
var Events_1 = require("../Events");
var ComponentViewModel_1 = require("../ComponentViewModel");
var ButtonViewModel_1 = require("../Html/ButtonViewModel");
var ModalErrorComponentViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(ModalErrorComponentViewModel, _super);
    function ModalErrorComponentViewModel() {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('modal-error-component', template)) || this;
        _this.title = ko.observable('');
        _this.isVisible = ko.observable(false);
        _this.modalOptions = new ModalOptionsViewModel_1.ModalOptionsViewModel();
        _this.errors = ko.observableArray([]);
        _this.errorSelectedEvents = new Events_1.ArrayItemEventCollection(_this.errors);
        _this.okCommand = new ButtonViewModel_1.ButtonViewModel();
        _this._errorSelected = new Events_1.DefaultEvent(_this);
        _this.errorSelected = new Events_1.DefaultEventHandler(_this._errorSelected);
        _this.errorSelectedEvents.register(function (e) { return e.errorSelected; }, _this.onErrorSelected.bind(_this));
        return _this;
    }
    ModalErrorComponentViewModel.prototype.onErrorSelected = function (error) {
        this._errorSelected.invoke(error);
    };
    return ModalErrorComponentViewModel;
}(ComponentViewModel_1.ComponentViewModel));
exports.ModalErrorComponentViewModel = ModalErrorComponentViewModel;
//# sourceMappingURL=ModalErrorComponentViewModel.js.map