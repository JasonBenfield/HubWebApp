"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var Alert_1 = require("../Alert");
var ContextualClass_1 = require("../ContextualClass");
var DropdownComponent_1 = require("../Dropdown/DropdownComponent");
var FaIcon_1 = require("../FaIcon");
var BlockViewModel_1 = require("../Html/BlockViewModel");
var TextSpan_1 = require("../Html/TextSpan");
var ListGroup_1 = require("../ListGroup/ListGroup");
var MarginCss_1 = require("../MarginCss");
var ErrorList_1 = require("./ErrorList");
var FormGroup_1 = require("../Html/FormGroup");
var PaddingCss_1 = require("../PaddingCss");
var SimpleFieldFormGroup = /** @class */ (function (_super) {
    tslib_1.__extends(SimpleFieldFormGroup, _super);
    function SimpleFieldFormGroup(prefix, name, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.name = prefix ? prefix + "_" + name : name;
        _this.dropdown = _this.inputGroup.addContent(new DropdownComponent_1.DropdownComponent());
        _this.dropdown.hide();
        _this.dropdown.button.setContext(ContextualClass_1.ContextualClass.danger);
        _this.dropdown.button.useOutlineStyle();
        _this.dropdown.button.addContent(new FaIcon_1.FaIcon('exclamation'))
            .configure(function (i) {
            i.solidStyle();
        });
        _this.dropdown.menu.setPadding(PaddingCss_1.PaddingCss.xs(0));
        var alertItem = _this.dropdown.menu.addItem();
        alertItem.addCssName(ContextualClass_1.ContextualClass.danger.append('border'));
        var alert = alertItem.addContent(new Alert_1.Alert());
        alert.setMargin(MarginCss_1.MarginCss.xs(0));
        alert.setContext(ContextualClass_1.ContextualClass.danger);
        _this.alertList = alert.addContent(new ListGroup_1.ListGroup());
        return _this;
    }
    SimpleFieldFormGroup.prototype.getName = function () {
        return this.name;
    };
    SimpleFieldFormGroup.prototype.getField = function (name) { return this.getName() === name ? this : null; };
    SimpleFieldFormGroup.prototype.setErrors = function (errors) {
        this.alertList.setItems(errors, function (e, li) {
            li.addCssName('dropdown-item-text');
            li.addCssName(ContextualClass_1.ContextualClass.danger.append('text'));
            li.addContent(new TextSpan_1.TextSpan(e.Message));
        });
        if (errors.length > 0) {
            this.dropdown.show();
        }
        else {
            this.dropdown.hide();
        }
    };
    SimpleFieldFormGroup.prototype.clearErrors = function () {
        this.setErrors([]);
    };
    SimpleFieldFormGroup.prototype.validate = function (errors) {
        var fieldErrors = new ErrorList_1.ErrorList();
        this.validateConstraints(fieldErrors);
        this.setErrors(fieldErrors.values());
        errors.merge(fieldErrors);
    };
    SimpleFieldFormGroup.prototype.import = function (values) {
        if (values) {
            var value = values[this.getName()];
            if (value !== undefined) {
                this.setValue(value);
            }
        }
    };
    SimpleFieldFormGroup.prototype.export = function (values) {
        values[this.getName()] = this.getValue();
    };
    return SimpleFieldFormGroup;
}(FormGroup_1.FormGroup));
exports.SimpleFieldFormGroup = SimpleFieldFormGroup;
//# sourceMappingURL=SimpleFieldFormGroup.js.map