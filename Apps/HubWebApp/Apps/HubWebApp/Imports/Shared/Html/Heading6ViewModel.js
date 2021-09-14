"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Heading6ViewModel = void 0;
var tslib_1 = require("tslib");
var HtmlComponentViewModel_1 = require("./HtmlComponentViewModel");
var template = require("./Heading6.html");
var ComponentTemplate_1 = require("../ComponentTemplate");
var AggregateComponentViewModel_1 = require("./AggregateComponentViewModel");
var Heading6ViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(Heading6ViewModel, _super);
    function Heading6ViewModel() {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('heading6', template)) || this;
        _this.content = new AggregateComponentViewModel_1.AggregateComponentViewModel();
        return _this;
    }
    return Heading6ViewModel;
}(HtmlComponentViewModel_1.HtmlComponentViewModel));
exports.Heading6ViewModel = Heading6ViewModel;
//# sourceMappingURL=Heading6ViewModel.js.map