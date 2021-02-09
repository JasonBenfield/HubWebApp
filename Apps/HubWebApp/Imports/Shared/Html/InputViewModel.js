"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var ComponentTemplate_1 = require("../ComponentTemplate");
var ko = require("knockout");
var template = require("./Input.html");
var HtmlComponentViewModel_1 = require("./HtmlComponentViewModel");
var InputViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(InputViewModel, _super);
    function InputViewModel() {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('input', template)) || this;
        _this.type = ko.observable('text');
        _this.value = ko.observable('');
        _this.maxLength = ko.observable(null);
        _this.isEnabled = ko.observable(true);
        _this.hasFocus = ko.observable(false);
        return _this;
    }
    return InputViewModel;
}(HtmlComponentViewModel_1.HtmlComponentViewModel));
exports.InputViewModel = InputViewModel;
//# sourceMappingURL=InputViewModel.js.map