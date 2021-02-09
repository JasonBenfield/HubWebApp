"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var ComponentTemplate_1 = require("../ComponentTemplate");
var ko = require("knockout");
var template = require("./Label.html");
var HtmlComponentViewModel_1 = require("./HtmlComponentViewModel");
var AggregateComponentViewModel_1 = require("./AggregateComponentViewModel");
var LabelViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(LabelViewModel, _super);
    function LabelViewModel() {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('label', template)) || this;
        _this.content = new AggregateComponentViewModel_1.AggregateComponentViewModel();
        _this.forTarget = ko.observable(null);
        return _this;
    }
    return LabelViewModel;
}(HtmlComponentViewModel_1.HtmlComponentViewModel));
exports.LabelViewModel = LabelViewModel;
//# sourceMappingURL=LabelViewModel.js.map