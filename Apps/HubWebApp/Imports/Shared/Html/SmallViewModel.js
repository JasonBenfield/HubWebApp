"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.SmallViewModel = void 0;
var tslib_1 = require("tslib");
var template = require("./Small.html");
var ComponentTemplate_1 = require("../ComponentTemplate");
var HtmlComponentViewModel_1 = require("./HtmlComponentViewModel");
var AggregateComponentViewModel_1 = require("./AggregateComponentViewModel");
var SmallViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(SmallViewModel, _super);
    function SmallViewModel() {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('small', template)) || this;
        _this.content = new AggregateComponentViewModel_1.AggregateComponentViewModel();
        return _this;
    }
    return SmallViewModel;
}(HtmlComponentViewModel_1.HtmlComponentViewModel));
exports.SmallViewModel = SmallViewModel;
//# sourceMappingURL=SmallViewModel.js.map