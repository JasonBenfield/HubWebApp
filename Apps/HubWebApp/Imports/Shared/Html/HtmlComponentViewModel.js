"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var ComponentViewModel_1 = require("../ComponentViewModel");
var ko = require("knockout");
var HtmlComponentViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(HtmlComponentViewModel, _super);
    function HtmlComponentViewModel(template) {
        var _this = _super.call(this, template) || this;
        _this.id = ko.observable(null);
        _this.name = ko.observable(null);
        _this.css = ko.observable('');
        _this.isVisible = ko.observable(true);
        _this.title = ko.observable(null);
        return _this;
    }
    return HtmlComponentViewModel;
}(ComponentViewModel_1.ComponentViewModel));
exports.HtmlComponentViewModel = HtmlComponentViewModel;
//# sourceMappingURL=HtmlComponentViewModel.js.map