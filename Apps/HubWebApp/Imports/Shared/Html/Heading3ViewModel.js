"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var HtmlComponentViewModel_1 = require("./HtmlComponentViewModel");
var template = require("./Heading3.html");
var ComponentTemplate_1 = require("../ComponentTemplate");
var AggregateComponentViewModel_1 = require("./AggregateComponentViewModel");
var Heading3ViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(Heading3ViewModel, _super);
    function Heading3ViewModel() {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('heading3', template)) || this;
        _this.content = new AggregateComponentViewModel_1.AggregateComponentViewModel();
        return _this;
    }
    return Heading3ViewModel;
}(HtmlComponentViewModel_1.HtmlComponentViewModel));
exports.Heading3ViewModel = Heading3ViewModel;
//# sourceMappingURL=Heading3ViewModel.js.map