"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Heading2ViewModel = void 0;
var tslib_1 = require("tslib");
var HtmlComponentViewModel_1 = require("./HtmlComponentViewModel");
var template = require("./Heading2.html");
var ComponentTemplate_1 = require("../ComponentTemplate");
var AggregateComponentViewModel_1 = require("./AggregateComponentViewModel");
var Heading2ViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(Heading2ViewModel, _super);
    function Heading2ViewModel() {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('heading2', template)) || this;
        _this.content = new AggregateComponentViewModel_1.AggregateComponentViewModel();
        return _this;
    }
    return Heading2ViewModel;
}(HtmlComponentViewModel_1.HtmlComponentViewModel));
exports.Heading2ViewModel = Heading2ViewModel;
//# sourceMappingURL=Heading2ViewModel.js.map