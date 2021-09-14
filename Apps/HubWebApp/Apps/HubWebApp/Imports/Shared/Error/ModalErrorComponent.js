"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ModalErrorComponent = void 0;
var ModalErrorComponentViewModel_1 = require("./ModalErrorComponentViewModel");
var Command_1 = require("../Command/Command");
var ModalErrorViewModel_1 = require("./ModalErrorViewModel");
var ModalErrorItemViewModel_1 = require("./ModalErrorItemViewModel");
var Enumerable_1 = require("../Enumerable");
var CssClass_1 = require("../CssClass");
var ContextualClass_1 = require("../ContextualClass");
var ModalErrorComponent = /** @class */ (function () {
    function ModalErrorComponent(vm) {
        var _this = this;
        if (vm === void 0) { vm = new ModalErrorComponentViewModel_1.ModalErrorComponentViewModel(); }
        this.vm = vm;
        this.errorSelected = this.vm.errorSelected;
        this.okCommand = new Command_1.Command(this.ok.bind(this))
            .configure(function (c) {
            c.addButton(_this.vm.okCommand)
                .configure(function (b) {
                b.setText('OK');
                b.setContext(ContextualClass_1.ContextualClass.danger);
            });
        });
        this.vm.modalOptions.closed.register(this.onClosed.bind(this));
    }
    ModalErrorComponent.prototype.addToContainer = function (container) {
        return container.addItem(this.vm, this);
    };
    ModalErrorComponent.prototype.insertIntoContainer = function (container, index) {
        return container.insertItem(index, this.vm, this);
    };
    ModalErrorComponent.prototype.removeFromContainer = function (container) {
        return container.removeItem(this);
    };
    ModalErrorComponent.prototype.onClosed = function () {
        this.vm.errors([]);
    };
    ModalErrorComponent.prototype.show = function (errors, caption) {
        if (caption === void 0) { caption = ''; }
        var errorVM = new ModalErrorViewModel_1.ModalErrorViewModel();
        errorVM.caption(caption);
        var anyCaptions = new Enumerable_1.Any(new Enumerable_1.FilteredArray(errors, function (e) { return Boolean(e.Caption); })).value();
        var captionCss = new CssClass_1.CssClass();
        var messageCss = new CssClass_1.CssClass();
        if (anyCaptions) {
            captionCss.addName('col-3');
            messageCss.addName('col');
        }
        var itemVMs = [];
        for (var _i = 0, errors_1 = errors; _i < errors_1.length; _i++) {
            var error = errors_1[_i];
            var itemVM = new ModalErrorItemViewModel_1.ModalErrorItemViewModel(error);
            itemVM.captionCss(captionCss.toString());
            itemVM.messageCss(messageCss.toString());
            itemVMs.push(itemVM);
        }
        errorVM.errors(itemVMs);
        this.vm.errors.splice(0, 0, errorVM);
        if (this.vm.errors().length === 1) {
            this.vm.title('An error occurred');
        }
        else {
            this.vm.title('Errors occurred');
        }
        this.vm.modalOptions.command('show');
    };
    ModalErrorComponent.prototype.ok = function () {
        this.vm.errors([]);
        this.vm.modalOptions.command('hide');
    };
    return ModalErrorComponent;
}());
exports.ModalErrorComponent = ModalErrorComponent;
//# sourceMappingURL=ModalErrorComponent.js.map