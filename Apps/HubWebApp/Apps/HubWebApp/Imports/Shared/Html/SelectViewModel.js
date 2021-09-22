"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.SelectViewModel = void 0;
var tslib_1 = require("tslib");
var ComponentTemplate_1 = require("../ComponentTemplate");
var HtmlComponentViewModel_1 = require("./HtmlComponentViewModel");
var template = require("./Select.html");
var ko = require("knockout");
var SelectViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(SelectViewModel, _super);
    function SelectViewModel() {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('select', template)) || this;
        _this.isEnabled = ko.observable(true);
        _this.value = ko.observable(null);
        _this.items = ko.observableArray([]);
        _this.itemsText = ko.observable('displayText');
        _this.itemsValue = ko.observable('value');
        _this.itemsCaption = ko.observable('');
        _this.hasFocus = ko.observable(false);
        return _this;
    }
    return SelectViewModel;
}(HtmlComponentViewModel_1.HtmlComponentViewModel));
exports.SelectViewModel = SelectViewModel;
//# sourceMappingURL=SelectViewModel.js.map