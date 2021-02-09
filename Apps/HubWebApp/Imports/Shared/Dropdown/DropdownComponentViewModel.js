"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var ComponentTemplate_1 = require("../ComponentTemplate");
var ComponentViewModel_1 = require("../ComponentViewModel");
var ButtonViewModel_1 = require("../Html/ButtonViewModel");
var UnorderedListViewModel_1 = require("../Html/UnorderedListViewModel");
var template = require("./DropdownComponent.html");
var ko = require("knockout");
var DropdownComponentViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(DropdownComponentViewModel, _super);
    function DropdownComponentViewModel() {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('dropdown-component', template)) || this;
        _this.isVisible = ko.observable(true);
        _this.button = new ButtonViewModel_1.ButtonViewModel();
        _this.menu = new UnorderedListViewModel_1.UnorderedListViewModel();
        return _this;
    }
    return DropdownComponentViewModel;
}(ComponentViewModel_1.ComponentViewModel));
exports.DropdownComponentViewModel = DropdownComponentViewModel;
//# sourceMappingURL=DropdownComponentViewModel.js.map