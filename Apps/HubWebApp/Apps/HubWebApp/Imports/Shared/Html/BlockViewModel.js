"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.BlockViewModel = void 0;
var tslib_1 = require("tslib");
var HtmlComponentViewModel_1 = require("./HtmlComponentViewModel");
var template = require("./Block.html");
var ComponentTemplate_1 = require("../ComponentTemplate");
var AggregateComponentViewModel_1 = require("./AggregateComponentViewModel");
var BlockViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(BlockViewModel, _super);
    function BlockViewModel() {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('block', template)) || this;
        _this.content = new AggregateComponentViewModel_1.AggregateComponentViewModel();
        return _this;
    }
    return BlockViewModel;
}(HtmlComponentViewModel_1.HtmlComponentViewModel));
exports.BlockViewModel = BlockViewModel;
//# sourceMappingURL=BlockViewModel.js.map