"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.DropdownBlockViewModel = void 0;
var tslib_1 = require("tslib");
var ComponentTemplate_1 = require("../ComponentTemplate");
var BlockViewModel_1 = require("../Html/BlockViewModel");
var ButtonViewModel_1 = require("../Html/ButtonViewModel");
var UnorderedListViewModel_1 = require("../Html/UnorderedListViewModel");
var template = require("./DropdownBlock.html");
var DropdownBlockViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(DropdownBlockViewModel, _super);
    function DropdownBlockViewModel() {
        var _this = _super.call(this) || this;
        _this.button = new ButtonViewModel_1.ButtonViewModel();
        _this.menu = new UnorderedListViewModel_1.UnorderedListViewModel();
        var componentTemplate = new ComponentTemplate_1.ComponentTemplate('dropdown-block', template);
        componentTemplate.register();
        _this.componentName(componentTemplate.name);
        return _this;
    }
    return DropdownBlockViewModel;
}(BlockViewModel_1.BlockViewModel));
exports.DropdownBlockViewModel = DropdownBlockViewModel;
//# sourceMappingURL=DropdownBlockViewModel.js.map