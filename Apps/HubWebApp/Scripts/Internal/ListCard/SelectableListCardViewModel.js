"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.SelectableListCardViewModel = void 0;
var tslib_1 = require("tslib");
var ComponentTemplate_1 = require("XtiShared/ComponentTemplate");
var template = require("./SelectableListCard.html");
var Events_1 = require("XtiShared/Events");
var ListCardViewModel_1 = require("./ListCardViewModel");
var SelectableListCardViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(SelectableListCardViewModel, _super);
    function SelectableListCardViewModel() {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('selectable-list-card', template)) || this;
        _this._itemSelected = new Events_1.DefaultEvent(_this);
        _this.itemSelected = _this._itemSelected.handler();
        return _this;
    }
    SelectableListCardViewModel.prototype.select = function (item) {
        this._itemSelected.invoke(item);
        return true;
    };
    return SelectableListCardViewModel;
}(ListCardViewModel_1.ListCardViewModel));
exports.SelectableListCardViewModel = SelectableListCardViewModel;
//# sourceMappingURL=SelectableListCardViewModel.js.map