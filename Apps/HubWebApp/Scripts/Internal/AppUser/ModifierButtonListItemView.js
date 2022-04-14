"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ModifierButtonListItemView = void 0;
var tslib_1 = require("tslib");
var TextBlockView_1 = require("@jasonbenfield/sharedwebapp/Html/TextBlockView");
var ButtonListGroupItemView_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ButtonListGroupItemView");
var ModifierButtonListItemView = /** @class */ (function (_super) {
    tslib_1.__extends(ModifierButtonListItemView, _super);
    function ModifierButtonListItemView() {
        var _this = _super.call(this) || this;
        _this.displayText = _this.addContent(new TextBlockView_1.TextBlockView);
        return _this;
    }
    return ModifierButtonListItemView;
}(ButtonListGroupItemView_1.ButtonListGroupItemView));
exports.ModifierButtonListItemView = ModifierButtonListItemView;
//# sourceMappingURL=ModifierButtonListItemView.js.map